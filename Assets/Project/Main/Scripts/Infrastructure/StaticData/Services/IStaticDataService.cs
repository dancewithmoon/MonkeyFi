using Base.States;
using Services;
using StaticData;
using UI.Windows;

namespace Infrastructure.StaticData.Services
{
    public interface IStaticDataService : IPreloadedInBootstrap
    {
        public ConfigStaticData GetConfig();
        public BaseWindow GetWindowPrefab(WindowType windowType);
    }
}