using System.Collections.Generic;
using System.Linq;
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
        private readonly Dictionary<string, LeaderboardEntryModel> _leaderboard = new();

        public List<LeaderboardEntryModel> Leaderboard => _leaderboard.Values.ToList();

        public PlayfabLeaderboardService(ClickerModel clickerModel)
        {
            _clickerModel = clickerModel;
        }

        public void LoadLeaderboard()
        {
            var request = new GetLeaderboardRequest
            {
                StatisticName = StatisticName
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