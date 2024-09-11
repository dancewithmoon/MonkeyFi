using System.Threading.Tasks;
using Base.AssetManagement;
using Base.Instantiating;
using Infrastructure.StaticData.Services;
using Services;
using UI.Windows;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : BaseFactory, IGameFactory
    {
        private const string UIRootPath = "UI/UIRoot";
        private const string HudOverlayPath = "UI/HUDOverlay";

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
        }

        public BaseWindow CreateWindow(WindowType windowType)
        {
            BaseWindow prefab = _staticDataService.GetWindowPrefab(windowType);
            return InstantiateRegistered(prefab, _uiRoot);
        }

        public async void CreateHudOverlay()
        {
            await InstantiateRegistered(HudOverlayPath, _uiRoot);
        }
    }
}