using Base.States;
using Services.UserProgress;

namespace Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private const string MainScene = "Main";
        
        private readonly IUserProgressService _progressService;

        public IGameStateMachine StateMachine { get; set; }

        public LoadProgressState(IUserProgressService progressService)
        {
            _progressService = progressService;
        }
        
        public void Enter()
        {
            _progressService.OnProgressLoadedEvent += OnProgressLoaded;
            _progressService.LoadProgress();
        }

        public void Exit()
        {
            _progressService.OnProgressLoadedEvent -= OnProgressLoaded;
        }

        private void OnProgressLoaded()
        {
            StateMachine.Enter<LoadLevelState, string>(MainScene);
        }
    }
}