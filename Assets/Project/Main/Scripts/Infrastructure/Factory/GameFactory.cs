using System.Threading.Tasks;
using Base.AssetManagement;
using Base.Instantiating;
using UnityEngine;

namespace Main.Infrastructure.Factory
{
    public class GameFactory : BaseFactory, IGameFactory
    {
        private const string UIRootPath = "UI/UIRoot";
        
        private GameObject _uiRoot;
        
        public GameFactory(IAssets assets, IInstantiateService instantiateService) : base(assets, instantiateService)
        {

        }

        public override async Task Preload()
        {
            await Task.WhenAll(assets.Load<GameObject>(UIRootPath));
        }

        public async void CreateUIRoot() => 
            _uiRoot = await InstantiateRegistered(UIRootPath);
    }
}