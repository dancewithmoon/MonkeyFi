using Base.States;
using Services;

namespace Infrastructure.States
{
    public class MonkeyBusinessState : IState
    {
        private readonly IWindowService _windowService;
        public IGameStateMachine StateMachine { get; set; }

        public MonkeyBusinessState(IWindowService windowService)
        {
            _windowService = windowService;
        }

        public void Enter() => 
            _windowService.Hud.OnBackButtonClickEvent += LoadMenu;

        public void Exit() => 
            _windowService.Hud.OnBackButtonClickEvent -= LoadMenu;

        private void LoadMenu() => 
            StateMachine.Enter<LoadMenuState>();
    }
}