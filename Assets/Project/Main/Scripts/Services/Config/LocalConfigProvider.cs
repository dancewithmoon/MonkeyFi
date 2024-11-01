using System.Threading.Tasks;
using Base.AssetManagement;
using Base.States;
using StaticData;

namespace Services.Config
{
    public class LocalConfigProvider : IConfigProvider, IPreloadedInBootstrap
    {
        private const string ConfigPath = "StaticData/Config";

        private readonly IAssets _assets;

        public ConfigData Config { get; private set; }
        
        public LocalConfigProvider(IAssets assets)
        {
            _assets = assets;
        }

        public async Task Preload() => 
            await LoadConfig();

        private async Task LoadConfig()
        {
            var staticData = await _assets.Load<ConfigStaticData>(ConfigPath);
            Config = new ConfigData(staticData.DefaultMaxEnergy, staticData.DefaultEnergyRechargePerSecond,
                staticData.SaveFrequencyInSeconds, staticData.StatisticsUpdateFrequencyInSeconds,
                staticData.LeaderboardSize, staticData.ShareMessage, staticData.ShareUrl, staticData.TonManifestUrl);
        }
    }
}