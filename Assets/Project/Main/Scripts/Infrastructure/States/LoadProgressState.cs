using Base.States;
using Models;
using Services.UserProgress;

namespace Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private const string MainScene = "Main";
        
        private readonly IUserProgressService _progressService;
        private readonly ClickerModel _clickerModel;

        public IGameStateMachine StateMachine { get; set; }

        public LoadProgressState(IUserProgressService progressService, ClickerModel clickerModel)
        {
            _progressService = progressService;
            _clickerModel = clickerModel;
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
            _clickerModel.UpdateValues(_progressService.ClickerPoints, _progressService.CurrentEnergy, _progressService.MaxEnergy);
            StateMachine.Enter<LoadLevelState, string>(MainScene);
        }
    }
}