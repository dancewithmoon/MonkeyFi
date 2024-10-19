﻿using System.Collections.Generic;
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

        public UserAuthorizationState(ITelegramService telegramService, IAuthorizationService authorizationService,
            List<IPreloadedAfterAuthorization> toPreload)
        {
            _telegramService = telegramService;
            _authorizationService = authorizationService;
            _toPreload = toPreload;
        }

        public async void Enter()
        {
            _telegramService.Initialize();
            await _authorizationService.Authorize();
            await PreloadServices();
            
            StateMachine.Enter<LoadProgressState>();
        }

        public void Exit()
        {
        }
        
        private Task PreloadServices() => 
            Task.WhenAll(_toPreload.Select(obj => obj.Preload()));
    }
}