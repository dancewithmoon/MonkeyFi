using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "Config", menuName = "ScriptableObjects/Config", order = 0)]
    public class ConfigStaticData : ScriptableObject
    {
        [SerializeField] private int _defaultMaxEnergy;
        [SerializeField] private int _defaultEnergyRechargePerSecond;
        [SerializeField] private int _saveFrequencyInSeconds;
        [SerializeField] private int _leaderboardSize;

        public int DefaultMaxEnergy => _defaultMaxEnergy;
        public int DefaultEnergyRechargePerSecond => _defaultEnergyRechargePerSecond;
        public int SaveFrequencyInSeconds => _saveFrequencyInSeconds;
        public int LeaderboardSize => _leaderboardSize;
    }
}