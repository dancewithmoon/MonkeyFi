using Base.States;
using UI.Windows;
using UnityEngine;

namespace Infrastructure.StaticData.Services
{
    public interface IStaticDataService : IPreloadedInBootstrap
    {
        public BaseWindow GetWindowPrefab(WindowType windowType);
        public Sprite GetWalletIcon(string appName);
        bool IconExists(string appName);
    }
}