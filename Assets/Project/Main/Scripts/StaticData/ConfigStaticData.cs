using System.Collections.Generic;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "Config", menuName = "ScriptableObjects/Config", order = 0)]
    public class ConfigStaticData : ScriptableObject
    {
        [SerializeField] private int _defaultMaxEnergy;
        [SerializeField] private int _defaultEnergyRechargePerSecond;
        [SerializeField] private int _saveFrequencyInSeconds;
        [SerializeField] private int _statisticsUpdateFrequencyInSeconds;
        [SerializeField] private int _leaderboardSize;

        [Header("Share")]
        [SerializeField] [TextArea] private string _shareMessage;
        [SerializeField] private string _shareUrl;

        [Header("TON")]
        [SerializeField] private string _tonManifestUrl;
        [SerializeField] private List<string> _supportedWallets;
        
        public int DefaultMaxEnergy => _defaultMaxEnergy;
        public int DefaultEnergyRechargePerSecond => _defaultEnergyRechargePerSecond;
        public int SaveFrequencyInSeconds => _saveFrequencyInSeconds;
        public int StatisticsUpdateFrequencyInSeconds => _statisticsUpdateFrequencyInSeconds;
        public int LeaderboardSize => _leaderboardSize;
        
        public string ShareMessage => _shareMessage;
        public string ShareUrl => _shareUrl;
        
        public string TonManifestUrl => _tonManifestUrl;
        public List<string> SupportedWallets => new(_supportedWallets);
    }
}