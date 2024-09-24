using System.Collections.Generic;
using Infrastructure.Factory;
using Services.Leaderboard;
using UI.Elements;
using UnityEngine;
using Zenject;

namespace UI.Windows
{
    public class LeaderboardWindow : BaseWindow
    {
        [SerializeField] private Transform _content;
        private ILeaderboardService _leaderboardService;
        private IGameFactory _gameFactory;
        private readonly Dictionary<LeaderboardEntryModel, LeaderboardItem> _entries = new();

        [Inject]
        private void Construct(ILeaderboardService leaderboardService, IGameFactory gameFactory)
        {
            _leaderboardService = leaderboardService;
            _gameFactory = gameFactory;
        }
        
        public override void DrawWindow()
        {
            _leaderboardService.Leaderboard.ForEach(DrawLeaderboardEntry);
        }

        private async void DrawLeaderboardEntry(LeaderboardEntryModel entryModel)
        {
            if (_entries.TryGetValue(entryModel, out LeaderboardItem item) == false)
            {
                item = await _gameFactory.CreateLeaderboardItem(entryModel, _content);
                _entries[entryModel] = item;
            }

            item.Draw();
        }
    }
}