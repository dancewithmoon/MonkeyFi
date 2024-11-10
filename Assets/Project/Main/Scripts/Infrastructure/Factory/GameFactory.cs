using System.Threading.Tasks;
using Base.AssetManagement;
using Base.Instantiating;
using Infrastructure.StaticData.Services;
using Services.Leaderboard;
using Services.Quests;
using Services.Quests.Conditions;
using Services.Referral;
using Services.TonWallet;
using UI;
using UI.Elements;
using UI.Windows;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.Factory
{
    public class GameFactory : BaseFactory, IGameFactory
    {
        private const string UIRootPath = "UI/UIRoot";
        private const string HudOverlayPath = "UI/HUDOverlay";
        private const string LeaderboardItemPath = "UI/Elements/LeaderboardItem";
        private const string FriendItemPath = "UI/Elements/FriendItem";
        private const string WalletItemPath = "UI/Elements/WalletItem";
        private const string QuestItemPath = "UI/Elements/QuestItem";
        private const string ConditionItemPath = "UI/Elements/ConditionItem";

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

        public async Task<LeaderboardItem> CreateLeaderboardItem(LeaderboardEntryModel model, Transform parent) => 
            await CreateItem<LeaderboardItem, LeaderboardEntryModel>(LeaderboardItemPath, model, parent);

        public async Task<FriendItem> CreateFriendItem(ReferralModel model, Transform parent) => 
            await CreateItem<FriendItem, ReferralModel>(FriendItemPath, model, parent);

        public async Task<WalletItem> CreateWalletItem(WalletModel model, Transform parent) => 
            await CreateItem<WalletItem, WalletModel>(WalletItemPath, model, parent);

        public async Task<QuestItem> CreateQuestItem(QuestModel model, Transform parent) => 
            await CreateItem<QuestItem, QuestModel>(QuestItemPath, model, parent);
        
        public async Task<ConditionItem> CreateConditionItem(ConditionModel model, Transform parent) => 
            await CreateItem<ConditionItem, ConditionModel>(ConditionItemPath, model, parent);

        private async Task<TItem> CreateItem<TItem, TModel>(string prefabPath, TModel model, Transform parent) 
            where TItem : BaseItem<TModel> 
            where TModel : class
        {
            GameObject itemObject = await InstantiateRegistered(prefabPath, parent);
            var item = itemObject.GetComponent<TItem>();
            item.Initialize(model);
            return item;
        }
    }
}