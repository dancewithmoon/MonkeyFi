using System.Collections.Generic;
using System.Threading.Tasks;
using Base.AssetManagement;
using StaticData;
using UI.Windows;
using UnityEngine;

namespace Infrastructure.StaticData.Services
{
    public class StaticDataService : IStaticDataService
    {
        private const string WindowsPath = "StaticData/WindowsStaticData";
        private const string WalletIconsPath = "StaticData/WalletIconsStaticData";
        
        private readonly IAssets _assets;
        
        private Dictionary<WindowType, BaseWindow> _windowPrefabs = new();
        private Dictionary<string, Sprite> _walletIcons = new();

        public StaticDataService(IAssets assets)
        {
            _assets = assets;
        }

        public async Task Preload() => 
            await Task.WhenAll(LoadWindows(), LoadWalletIcons());

        public BaseWindow GetWindowPrefab(WindowType windowType) => 
            _windowPrefabs[windowType];

        public Sprite GetWalletIcon(string appName) => 
            _walletIcons[appName];

        public bool IconExists(string appName) => 
            _walletIcons.ContainsKey(appName);

        private async Task LoadWindows()
        {
            var staticData = await _assets.Load<WindowsStaticData>(WindowsPath);
            _windowPrefabs = new Dictionary<WindowType, BaseWindow>(staticData.WindowPrefabs);
        }
        
        private async Task LoadWalletIcons()
        {
            var staticData = await _assets.Load<WalletIconsStaticData>(WalletIconsPath);
            _walletIcons = new Dictionary<string, Sprite>(staticData.Icons);
        }
    }
}