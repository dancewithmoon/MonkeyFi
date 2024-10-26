using System.Threading.Tasks;
using Base.AssetManagement;
using Base.States;
using StaticData;

namespace Services.Config
{
    public class LocalConfigService : IConfigService, IPreloadedInBootstrap
    {
        private const string ConfigPath = "StaticData/Config";

        private readonly IAssets _assets;

        public ConfigModel Config { get; private set; }
        
        public LocalConfigService(IAssets assets)
        {
            _assets = assets;
        }

        public async Task Preload() => 
            await LoadConfig();

        private async Task LoadConfig()
        {
            var staticData = await _assets.Load<ConfigStaticData>(ConfigPath);
            Config = new ConfigModel(staticData.DefaultMaxEnergy, staticData.DefaultEnergyRechargePerSecond,
                staticData.SaveFrequencyInSeconds, staticData.StatisticsUpdateFrequencyInSeconds,
                staticData.LeaderboardSize, staticData.ShareMessage, staticData.ShareUrl);
        }
    }
}