using System.Collections.Generic;
using System.Threading.Tasks;
using Base.AssetManagement;
using Services;
using StaticData;
using UI.Windows;

namespace Infrastructure.StaticData.Services
{
    public class StaticDataService : IStaticDataService
    {
        private const string ConfigPath = "StaticData/Config";
        private const string WindowsPath = "StaticData/WindowsStaticData";
        
        private readonly IAssets _assets;

        private ConfigStaticData _config;
        private Dictionary<WindowType, BaseWindow> _windowPrefabs = new();

        public StaticDataService(IAssets assets)
        {
            _assets = assets;
        }

        public async Task Preload()
        {
            await Task.WhenAll(LoadConfig(), LoadWindows());
        }

        public ConfigStaticData GetConfig() => _config;

        public BaseWindow GetWindowPrefab(WindowType windowType) => 
            _windowPrefabs[windowType];

        private async Task LoadConfig() => 
            _config = await _assets.Load<ConfigStaticData>(ConfigPath);

        private async Task LoadWindows()
        {
            var staticData = await _assets.Load<WindowsStaticData>(WindowsPath);
            _windowPrefabs = new Dictionary<WindowType, BaseWindow>(staticData.WindowPrefabs);
        }
    }
}