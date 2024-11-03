using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.Services.CoroutineRunner;
using Cysharp.Threading.Tasks;
using Infrastructure.StaticData.Services;
using Services.Config;
using TonSdk.Connect;
using TonSdk.Core;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;

namespace Services.TonWallet
{
    public class TonWalletService
    {
        private const string WalletsListUrl = "https://raw.githubusercontent.com/ton-blockchain/wallets-list/main/wallets-v2.json";
        private readonly IConfigProvider _configProvider;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IStaticDataService _staticData;

        private TonConnectHandler _tonConnectHandler;
        private Dictionary<string, WalletConfig> _walletConfigs;
        private KeyValuePair<Address, string> _userFriendlyWalletAddress;
        
        public Dictionary<string, WalletModel> Wallets { get; private set; }

        public bool IsConnected => _tonConnectHandler.tonConnect.IsConnected;
        public string WalletAddress
        {
            get
            {
                Address address = _tonConnectHandler.tonConnect.Wallet.Account.Address;
                if (_userFriendlyWalletAddress.Key != address)
                    _userFriendlyWalletAddress = new KeyValuePair<Address, string>(address, address.ToUserFriendlyAddress());
                
                return _userFriendlyWalletAddress.Value;
            }
        }

        public event Action OnWalletUpdateEvent;
        
        public TonWalletService(IConfigProvider configProvider, ICoroutineRunner coroutineRunner, IStaticDataService staticData)
        {
            _configProvider = configProvider;
            _coroutineRunner = coroutineRunner;
            _staticData = staticData;
        }

        public async Task Initialize()
        {
            _tonConnectHandler = CreateTonConnectHandler();
            TonConnectHandler.OnProviderStatusChanged += OnProviderStatusChange;
            TonConnectHandler.OnProviderStatusChangedError += OnProviderStatusChangeError;
            
            InitializeTonConnect();
            await LoadWallets();
        }

        public async void ConnectWallet(WalletModel walletModel)
        {
            string url = await _tonConnectHandler.tonConnect.Connect(_walletConfigs[walletModel.AppName]);
            string escapedUrl = Uri.EscapeUriString(url);
            Application.OpenURL(escapedUrl);
        }

        public async void DisconnectWallet()
        {
            _tonConnectHandler.RestoreConnectionOnAwake = false;
            await _tonConnectHandler.tonConnect.Disconnect();
        }

        private void OnProviderStatusChange(Wallet wallet) => 
            OnWalletUpdateEvent?.Invoke();

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
            Wallets = CreateWalletModels();
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
                if (wallet.BridgeUrl == null)
                    continue;

                if(!_staticData.IconExists(wallet.AppName))
                    continue;
                
                walletConfigs[wallet.AppName] = wallet;
            }

            return walletConfigs;
        }

        private Dictionary<string, WalletModel> CreateWalletModels()
        {
            var walletModels = new Dictionary<string, WalletModel>();
            foreach ((string key, WalletConfig walletConfig) in _walletConfigs)
                walletModels[key] = new WalletModel(walletConfig.AppName, walletConfig.Name, _staticData.GetWalletIcon(key));

            return walletModels;
        }

        private static TonConnectHandler CreateTonConnectHandler()
        {
            var handler =  new GameObject("TonConnectHandler").AddComponent<TonConnectHandler>();
            Object.DontDestroyOnLoad(handler);
            return handler;
        }
    }
}