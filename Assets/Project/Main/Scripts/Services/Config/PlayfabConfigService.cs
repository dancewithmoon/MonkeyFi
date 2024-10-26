using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.States;
using PlayFab.ClientModels;
using Utils;

namespace Services.Config
{
    public class PlayfabConfigService : IConfigService, IPreloadedAfterAuthorization
    {
        public ConfigModel Config { get; private set; }
        
        public async Task Preload()
        {
            GetTitleDataResult result = await PlayFabClientAsyncAPI.GetTitleData(new GetTitleDataRequest());
            Config = ParseRawConfig(result.Data);
        }

        private ConfigModel ParseRawConfig(Dictionary<string, string> rawConfig)
        {
            int defaultMaxEnergy = int.Parse(rawConfig[nameof(Config.DefaultMaxEnergy)]);
            int defaultEnergyRechargePerSecond = int.Parse(rawConfig[nameof(Config.DefaultEnergyRechargePerSecond)]);
            int saveFrequencyInSeconds = int.Parse(rawConfig[nameof(Config.SaveFrequencyInSeconds)]);
            int statisticsUpdateFrequencyInSeconds = int.Parse(rawConfig[nameof(Config.StatisticsUpdateFrequencyInSeconds)]);
            int leaderboardSize = int.Parse(rawConfig[nameof(Config.LeaderboardSize)]);
            string shareMessage = rawConfig[nameof(Config.ShareMessage)];
            string shareUrl = rawConfig[nameof(Config.ShareUrl)];
            
            return new ConfigModel(defaultMaxEnergy, defaultEnergyRechargePerSecond, saveFrequencyInSeconds,
                statisticsUpdateFrequencyInSeconds, leaderboardSize, shareMessage, shareUrl);
        }
    }
}