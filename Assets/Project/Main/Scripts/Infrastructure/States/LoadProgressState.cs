using System.Threading.Tasks;
using Base.States;
using Services.Leaderboard;
using Services.Referral;
using Services.UserProgress;

namespace Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly IUserProgressService _progressService;
        private readonly ILeaderboardService _leaderboardService;
        private readonly IReferralService _referralService;

        public IGameStateMachine StateMachine { get; set; }

        public LoadProgressState(IUserProgressService progressService, ILeaderboardService leaderboardService, IReferralService referralService)
        {
            _progressService = progressService;
            _leaderboardService = leaderboardService;
            _referralService = referralService;
        }
        
        public async void Enter()
        {
            await Task.WhenAll(
                _progressService.LoadProgress(),
                _referralService.LoadReferrals());

            _leaderboardService.LoadLeaderboard();
            _leaderboardService.Initialize();
            OnProgressLoaded();
        }

        public void Exit()
        {
        }

        private void OnProgressLoaded() => 
            StateMachine.Enter<LoadMenuState>();
    }
}