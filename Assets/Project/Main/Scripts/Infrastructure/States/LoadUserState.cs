using Base.States;
using Services.Telegram;

namespace Infrastructure.States
{
    public class LoadUserState : IState
    {
        private readonly ITelegramService _telegramService;
        public IGameStateMachine StateMachine { get; set; }

        public LoadUserState(ITelegramService telegramService)
        {
            _telegramService = telegramService;
        }

        public void Enter()
        {
            _telegramService.OnUserDataLoadedEvent += OnUserDataLoaded;
            _telegramService.Initialize();
        }

        public void Exit()
        {
            _telegramService.OnUserDataLoadedEvent -= OnUserDataLoaded;
        }

        private void OnUserDataLoaded() => 
            StateMachine.Enter<LoadProgressState>();
    }
}