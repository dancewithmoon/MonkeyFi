using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.States;
using Models;
using PlayFab.ClientModels;
using Services.Config;
using UnityEngine;
using Utils;

namespace Services.Leaderboard
{
    public class PlayfabLeaderboardService : ILeaderboardService, IPreloadedInLoadMenu
    {
        private const string StatisticName = "Leaderboard";
        
        private readonly ClickerModel _clickerModel;
        private readonly IConfigProvider _configProvider;
        private readonly Dictionary<string, LeaderboardEntryModel> _leaderboard = new();
        
        public List<LeaderboardEntryModel> Leaderboard => _leaderboard.Values.ToList();
        private int _previousPointsValue;

        public PlayfabLeaderboardService(ClickerModel clickerModel, IConfigProvider configProvider)
        {
            _clickerModel = clickerModel;
            _configProvider = configProvider;
        }
        
        public Task Preload()
        {
            _previousPointsValue = _clickerModel.Points;
            return Task.CompletedTask;
        }

        public async void LoadLeaderboard()
        {
            var request = new GetLeaderboardRequest
            {
                StatisticName = StatisticName,
                MaxResultsCount = _configProvider.Config.LeaderboardSize
            };

            GetLeaderboardResult result = await PlayFabClientAsyncAPI.GetLeaderboard(request);
            foreach (PlayerLeaderboardEntry entry in result.Leaderboard)
                _leaderboard[entry.PlayFabId] = new LeaderboardEntryModel(entry.DisplayName, entry.StatValue);
        }

        public async void UpdatePlayerStatistics()
        {
            if(_clickerModel.Points == _previousPointsValue)
                return;
            
            var request = new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>
                {
                    new()
                    {
                        StatisticName = StatisticName,
                        Value = _clickerModel.Points
                    }
                }
            };

            UpdatePlayerStatisticsResult result = await PlayFabClientAsyncAPI.UpdatePlayerStatistics(request);
            _previousPointsValue = _clickerModel.Points;
            Debug.Log("Player statistics sent: " + result.ToJson());
        }
    }
}