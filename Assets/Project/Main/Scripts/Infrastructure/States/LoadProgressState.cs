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
        
        public void Enter()
        {
            _progressService.OnProgressLoadedEvent += OnProgressLoaded;
            _progressService.LoadProgress();
            _leaderboardService.LoadLeaderboard();
        }

        public void Exit()
        {
            _progressService.OnProgressLoadedEvent -= OnProgressLoaded;
        }

        private void OnProgressLoaded()
        {
            StateMachine.Enter<LoadMenuState>();
        }
    }
}