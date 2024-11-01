using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.Services.CoroutineRunner;
using Cysharp.Threading.Tasks;
using Services.Config;
using TonSdk.Connect;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

namespace Services.TonWallet
{
    public class TonWalletService
    {
        private const string WalletsListUrl = "https://raw.githubusercontent.com/ton-blockchain/wallets-list/main/wallets-v2.json";
        private readonly IConfigProvider _configProvider;
        private readonly ICoroutineRunner _coroutineRunner;
        
        private TonConnectHandler _tonConnectHandler;
        private Dictionary<string, WalletConfig> _walletConfigs;
        
        public Dictionary<string, WalletModel> Wallets { get; private set; }

        public bool IsConnected => _tonConnectHandler.tonConnect.IsConnected;
        
        public TonWalletService(IConfigProvider configProvider, ICoroutineRunner coroutineRunner)
        {
            _configProvider = configProvider;
            _coroutineRunner = coroutineRunner;
        }

        public async Task Initialize()
        {
            _tonConnectHandler = CreateTonConnectHandler();
            TonConnectHandler.OnProviderStatusChanged += OnProviderStatusChange;
            TonConnectHandler.OnProviderStatusChangedError += OnProviderStatusChangeError;
            
            InitializeTonConnect();
            await LoadWallets();
        }

        public void ConnectWallet() { }

        private void OnProviderStatusChange(Wallet wallet) { }

        private void OnProviderStatusChangeError(string error) { }
        
        private void InitializeTonConnect()
        {
            _tonConnectHandler.ManifestURL = _configProvider.Config.TonManifestUrl;
            _tonConnectHandler.UseWebWallets = true;
            _tonConnectHandler.Initialize();
        }

        private async Task LoadWallets()
        {
            List<WalletConfig> rawWalletConfigs = await LoadWalletConfigs();
            _walletConfigs = ParseWalletConfigs(rawWalletConfigs);
            Wallets = await CreateWalletModels();
        }

        private async Task<List<WalletConfig>> LoadWalletConfigs()
        {
            List<WalletConfig> wallets = null;
            _coroutineRunner.StartCoroutine(_tonConnectHandler.LoadWallets(WalletsListUrl, result => wallets = result));
            await UniTask.WaitUntil(() => wallets != null);
            return wallets;
        }

        private Dictionary<string, WalletConfig> ParseWalletConfigs(List<WalletConfig> wallets)
        {
            var walletConfigs = new Dictionary<string, WalletConfig>();
            foreach (WalletConfig wallet in wallets)
            {
                if (!_configProvider.Config.SupportedWallets.Contains(wallet.AppName))
                    continue;

                if (wallet.BridgeUrl == null)
                    continue;

                walletConfigs[wallet.AppName] = wallet;
            }

            return walletConfigs;
        }

        private async Task<Dictionary<string, WalletModel>> CreateWalletModels()
        {
            var walletModels = new Dictionary<string, WalletModel>();
            foreach ((string key, WalletConfig walletConfig) in _walletConfigs)
                walletModels[key] = await CreateWalletModel(walletConfig);

            return walletModels;
        }

        private static TonConnectHandler CreateTonConnectHandler()
        {
            var handler =  new GameObject("TonConnectHandler").AddComponent<TonConnectHandler>();
            Object.DontDestroyOnLoad(handler);
            return handler;
        }

        private static async Task<WalletModel> CreateWalletModel(WalletConfig walletConfig)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(walletConfig.Image);
            await request.SendWebRequest();
            if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
                throw new Exception("Error while loading wallet image: " + request.error);

            Texture2D iconTexture = DownloadHandlerTexture.GetContent(request);
            Sprite iconSprite = Sprite.Create(iconTexture, new Rect(0.0f, 0.0f, iconTexture.width, iconTexture.height), 
                new Vector2(0.5f, 0.5f), 100.0f);
            
            return new WalletModel(walletConfig.AppName, walletConfig.Name, iconSprite);
        }
    }
}