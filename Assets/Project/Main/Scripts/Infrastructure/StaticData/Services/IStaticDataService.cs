using Base.States;
using Services;
using UI.Windows;

namespace Infrastructure.StaticData.Services
{
    public interface IStaticDataService : IPreloadedInBootstrap
    {
        public BaseWindow GetWindowPrefab(WindowType windowType);
    }
}