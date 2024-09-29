using System.Threading.Tasks;
using Base.AssetManagement;
using Base.Instantiating;
using UnityEngine;

namespace MonkeyBusiness.Infrastructure.Factory
{
    public class MonkeyBusinessFactory : BaseFactory, IMonkeyBusinessFactory
    {
        private const string HeroPath = "Hero";

        public MonkeyBusinessFactory(IAssets assets, IInstantiateService instantiateService) : base(assets,
            instantiateService)
        {
        }

        public override async Task Preload()
        {
            await Task.WhenAll(assets.Load<GameObject>(HeroPath));
        }

        public async Task<GameObject> CreateHero(Vector3 at)
        {
            GameObject hero = await InstantiateRegistered(HeroPath, at);
            return hero;
        }
    }
}