using Base.States;
using Services.Leaderboard;
using Services.UserProgress;

namespace Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly IUserProgressService _progressService;
        private readonly ILeaderboardService _leaderboardService;

        public IGameStateMachine StateMachine { get; set; }

        public LoadProgressState(IUserProgressService progressService, ILeaderboardService leaderboardService)
        {
            _progressService = progressService;
            _leaderboardService = leaderboardService;
        }
        
        public async void Enter()
        {
            await _progressService.LoadProgress();
            _leaderboardService.LoadLeaderboard();
            OnProgressLoaded();
        }

        public void Exit()
        {
        }

        private void OnProgressLoaded() => 
            StateMachine.Enter<LoadMenuState>();
    }
}