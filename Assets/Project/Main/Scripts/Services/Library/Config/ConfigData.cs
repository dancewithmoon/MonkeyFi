namespace Services.Library.Config
{
    public class ConfigData
    {
        public int DefaultMaxEnergy { get; }
        public int DefaultEnergyRechargePerSecond { get; }
        public int SaveFrequencyInSeconds { get; }
        public int StatisticsUpdateFrequencyInSeconds { get; }
        public int LeaderboardSize { get; }
        public string ShareMessage { get; }
        public string ShareUrl { get; }
        public string TonManifestUrl { get; }
        public string WalletAddress { get; }
        public float CheckInCost { get; }
        public string BotName { get; }

        public ConfigData(int defaultMaxEnergy, int defaultEnergyRechargePerSecond, int saveFrequencyInSeconds,
            int statisticsUpdateFrequencyInSeconds, int leaderboardSize, string shareMessage, string shareUrl,
            string tonManifestUrl, string walletAddress, float checkInCost, string botName)
        {
            DefaultMaxEnergy = defaultMaxEnergy;
            DefaultEnergyRechargePerSecond = defaultEnergyRechargePerSecond;
            SaveFrequencyInSeconds = saveFrequencyInSeconds;
            StatisticsUpdateFrequencyInSeconds = statisticsUpdateFrequencyInSeconds;
            LeaderboardSize = leaderboardSize;
            ShareMessage = shareMessage;
            ShareUrl = shareUrl;
            TonManifestUrl = tonManifestUrl;
            WalletAddress = walletAddress;
            CheckInCost = checkInCost;
            BotName = botName;
        }
    }
}