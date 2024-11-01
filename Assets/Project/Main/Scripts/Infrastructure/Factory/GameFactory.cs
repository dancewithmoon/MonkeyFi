using System.Threading.Tasks;
using Base.AssetManagement;
using Base.Instantiating;
using Infrastructure.StaticData.Services;
using Services.Leaderboard;
using Services.Referral;
using Services.TonWallet;
using UI;
using UI.Elements;
using UI.Windows;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : BaseFactory, IGameFactory
    {
        private const string UIRootPath = "UI/UIRoot";
        private const string HudOverlayPath = "UI/HUDOverlay";
        private const string LeaderboardItemPath = "UI/Elements/LeaderboardItem";
        private const string FriendItemPath = "UI/Elements/FriendItem";
        private const string WalletItemPath = "UI/Elements/WalletItem";

        private readonly IStaticDataService _staticDataService;
        private Transform _uiRoot;
        
        public GameFactory(IAssets assets, IInstantiateService instantiateService, IStaticDataService staticDataService) : base(assets, instantiateService)
        {
            _staticDataService = staticDataService;
        }

        public override async Task Preload()
        {
            await Task.WhenAll(
                assets.Load<GameObject>(UIRootPath), 
                assets.Load<GameObject>(HudOverlayPath));
        }

        public async void CreateUIRoot()
        {
            GameObject uiRootObj = await InstantiateRegistered(UIRootPath);
            _uiRoot = uiRootObj.transform;
            Object.DontDestroyOnLoad(_uiRoot);
        }

        public BaseWindow CreateWindow(WindowType windowType)
        {
            BaseWindow prefab = _staticDataService.GetWindowPrefab(windowType);
            BaseWindow window = InstantiateRegistered(prefab, _uiRoot);
            window.OnWindowCreated();
            return window;
        }

        public async Task<HudOverlay> CreateHudOverlay()
        {
            GameObject hudObject = await InstantiateRegistered(HudOverlayPath, _uiRoot);
            var hud = hudObject.GetComponent<HudOverlay>();
            hud.OnWindowCreated();
            return hud;
        }

        public async Task<LeaderboardItem> CreateLeaderboardItem(LeaderboardEntryModel model, Transform parent)
        {
            GameObject leaderboardObject = await InstantiateRegistered(LeaderboardItemPath, parent);
            var leaderboardItem = leaderboardObject.GetComponent<LeaderboardItem>();
            leaderboardItem.Initialize(model);
            return leaderboardItem;
        }
        
        public async Task<FriendItem> CreateFriendItem(ReferralModel model, Transform parent)
        {
            GameObject friendObject = await InstantiateRegistered(FriendItemPath, parent);
            var friendItem = friendObject.GetComponent<FriendItem>();
            friendItem.Initialize(model);
            return friendItem;
        }
        
        public async Task<WalletItem> CreateWalletItem(WalletModel model, Transform parent)
        {
            GameObject walletObject = await InstantiateRegistered(WalletItemPath, parent);
            var walletItem = walletObject.GetComponent<WalletItem>();
            walletItem.Initialize(model);
            return walletItem;
        }
    }
}