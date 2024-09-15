using Base.States;
using Services.Login;
using Services.Telegram;

namespace Infrastructure.States
{
    public class UserAuthorizationState : IState
    {
        private readonly ITelegramService _telegramService;
        private readonly IAuthorizationService _authorizationService;
        public IGameStateMachine StateMachine { get; set; }

        public UserAuthorizationState(ITelegramService telegramService, IAuthorizationService authorizationService)
        {
            _telegramService = telegramService;
            _authorizationService = authorizationService;
        }

        public void Enter()
        {
            _telegramService.OnUserDataLoadedEvent += OnUserDataLoaded;
            _authorizationService.OnAuthorizationSuccessEvent += OnAuthorizationSuccess;
            _telegramService.Initialize();
        }

        public void Exit()
        {
            _telegramService.OnUserDataLoadedEvent -= OnUserDataLoaded;
            _authorizationService.OnAuthorizationSuccessEvent -= OnAuthorizationSuccess;
        }

        private void OnUserDataLoaded() => 
            _authorizationService.Authorize();

        private void OnAuthorizationSuccess() => 
            StateMachine.Enter<LoadProgressState>();
    }
}