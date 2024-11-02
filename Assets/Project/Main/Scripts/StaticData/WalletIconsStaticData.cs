using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "WalletIconsStaticData", menuName = "ScriptableObjects/WalletIconsStaticData", order = 0)]
    public class WalletIconsStaticData : ScriptableObject
    {
        [SerializeField] private SerializedDictionary<string, Sprite> _icons;

        public Dictionary<string, Sprite> Icons => _icons;
    }
}