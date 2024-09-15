using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.States;
using Services.Login;
using Services.Telegram;

namespace Infrastructure.States
{
    public class UserAuthorizationState : IState
    {
        private readonly ITelegramService _telegramService;
        private readonly IAuthorizationService _authorizationService;
        private readonly List<IPreloadedAfterAuthorization> _toPreload;
        public IGameStateMachine StateMachine { get; set; }

        public UserAuthorizationState(ITelegramService telegramService, IAuthorizationService authorizationService, List<IPreloadedAfterAuthorization> toPreload)
        {
            _telegramService = telegramService;
            _authorizationService = authorizationService;
            _toPreload = toPreload;
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

        private async void OnAuthorizationSuccess()
        {
            await Task.WhenAll(_toPreload.Select(obj => obj.Preload()));
            StateMachine.Enter<LoadProgressState>();
        }
    }
}