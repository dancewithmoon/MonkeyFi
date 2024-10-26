using System.Collections.Generic;
using System.Linq;
using Models;
using PlayFab.ClientModels;
using Services.Config;
using UnityEngine;
using Utils;

namespace Services.Leaderboard
{
    public class PlayfabLeaderboardService : ILeaderboardService
    {
        private const string StatisticName = "Leaderboard";
        
        private readonly ClickerModel _clickerModel;
        private readonly IConfigService _configService;
        private readonly Dictionary<string, LeaderboardEntryModel> _leaderboard = new();

        public List<LeaderboardEntryModel> Leaderboard => _leaderboard.Values.ToList();

        public PlayfabLeaderboardService(ClickerModel clickerModel, IConfigService configService)
        {
            _clickerModel = clickerModel;
            _configService = configService;
        }

        public async void LoadLeaderboard()
        {
            var request = new GetLeaderboardRequest
            {
                StatisticName = StatisticName,
                MaxResultsCount = _configService.Config.LeaderboardSize
            };

            GetLeaderboardResult result = await PlayFabClientAsyncAPI.GetLeaderboard(request);
            foreach (PlayerLeaderboardEntry entry in result.Leaderboard)
                _leaderboard[entry.PlayFabId] = new LeaderboardEntryModel(entry.DisplayName, entry.StatValue);
        }

        public async void UpdatePlayerStatistics()
        {
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
            Debug.Log("Player statistics sent: " + result.ToJson());
        }
    }
}