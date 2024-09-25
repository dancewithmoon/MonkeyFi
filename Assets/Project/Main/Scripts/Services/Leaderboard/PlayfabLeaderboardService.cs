using System.Collections.Generic;
using System.Linq;
using Infrastructure.StaticData.Services;
using Models;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace Services.Leaderboard
{
    public class PlayfabLeaderboardService : ILeaderboardService
    {
        private const string StatisticName = "Leaderboard";
        
        private readonly ClickerModel _clickerModel;
        private readonly IStaticDataService _staticDataService;
        private readonly Dictionary<string, LeaderboardEntryModel> _leaderboard = new();

        public List<LeaderboardEntryModel> Leaderboard => _leaderboard.Values.ToList();

        public PlayfabLeaderboardService(ClickerModel clickerModel, IStaticDataService staticDataService)
        {
            _clickerModel = clickerModel;
            _staticDataService = staticDataService;
        }

        public void LoadLeaderboard()
        {
            var request = new GetLeaderboardRequest
            {
                StatisticName = StatisticName,
                MaxResultsCount = _staticDataService.GetConfig().LeaderboardSize
            };

            PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardReceived, OnError);
        }

        public void UpdatePlayerStatistics()
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
            PlayFabClientAPI.UpdatePlayerStatistics(request, OnRequestSent, OnError);
        }

        private void OnLeaderboardReceived(GetLeaderboardResult result)
        {
            foreach (PlayerLeaderboardEntry entry in result.Leaderboard)
                _leaderboard[entry.PlayFabId] = new LeaderboardEntryModel(entry.DisplayName, entry.StatValue);
        }

        private void OnRequestSent(UpdatePlayerStatisticsResult result) => 
            Debug.Log("Player statistics sent: " + result.ToJson());
        
        private void OnError(PlayFabError error) => 
            Debug.LogError("Playfab error: " + error);
    }
}