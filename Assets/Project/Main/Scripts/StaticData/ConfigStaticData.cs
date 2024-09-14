using UnityEngine;
using UnityEngine.Serialization;

namespace StaticData
{
    [CreateAssetMenu(fileName = "Config", menuName = "ScriptableObjects/Config", order = 0)]
    public class ConfigStaticData : ScriptableObject
    {
        [SerializeField] private int _defaultMaxEnergy;
        [SerializeField] private int _defaultEnergyRechargePerSecond;
        
        public int DefaultMaxEnergy => _defaultMaxEnergy;
        public int DefaultEnergyRechargePerSecond => _defaultEnergyRechargePerSecond;
    }
}