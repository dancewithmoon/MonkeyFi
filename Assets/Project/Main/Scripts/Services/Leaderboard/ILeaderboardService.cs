using System.Collections.Generic;

namespace Services.Leaderboard
{
    public interface ILeaderboardService
    {
        List<LeaderboardEntryModel> Leaderboard { get; }
        void UpdatePlayerStatistics();
        void LoadLeaderboard();
    }
}