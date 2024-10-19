using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.States;
using Services.Login;
using Services.Referral;
using Services.Telegram;

namespace Infrastructure.States
{
    public class UserAuthorizationState : IState
    {
        private readonly ITelegramService _telegramService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IReferralService _referralService;
        private readonly List<IPreloadedAfterAuthorization> _toPreload;
        public IGameStateMachine StateMachine { get; set; }

        public UserAuthorizationState(ITelegramService telegramService, IAuthorizationService authorizationService, IReferralService referralService,
            List<IPreloadedAfterAuthorization> toPreload)
        {
            _telegramService = telegramService;
            _authorizationService = authorizationService;
            _referralService = referralService;
            _toPreload = toPreload;
        }

        public async void Enter()
        {
            _telegramService.Initialize();
            await _authorizationService.Authorize();
            await PreloadServices();

            if (string.IsNullOrEmpty(_telegramService.ReferralCode) == false)
                _referralService.GetReferrer(_telegramService.ReferralCode);
            
            StateMachine.Enter<LoadProgressState>();
        }

        public void Exit()
        {
        }
        
        private Task PreloadServices() => 
            Task.WhenAll(_toPreload.Select(obj => obj.Preload()));
    }
}