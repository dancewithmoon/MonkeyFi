using System.Threading.Tasks;
using Services;
using Services.Leaderboard;
using UI.Elements;
using UI.Windows;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory
    {
        void CreateUIRoot();
        public BaseWindow CreateWindow(WindowType windowType);
        void CreateHudOverlay();
        Task<LeaderboardItem> CreateLeaderboardItem(LeaderboardEntryModel model, Transform parent);
    }
}