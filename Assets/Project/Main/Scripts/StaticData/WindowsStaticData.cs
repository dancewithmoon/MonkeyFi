using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Services;
using UI.Windows;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "WindowsStaticData", menuName = "ScriptableObjects/WindowsStaticData", order = 1)]
    public class WindowsStaticData : ScriptableObject
    {
        [SerializedDictionary("Window type", "Prefab")]
        [SerializeField] private SerializedDictionary<WindowType, BaseWindow> _windowPrefabs;

        public Dictionary<WindowType, BaseWindow> WindowPrefabs => _windowPrefabs;
    }
}