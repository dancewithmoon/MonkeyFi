﻿using System;
using Base;
using Base.States;
using Infrastructure.Factory;
using Infrastructure.States;
using Infrastructure.StaticData.Services;
using Models;
using Services;
using Services.Leaderboard;
using Services.Login;
using Services.Telegram;
using Services.Time;
using Services.UserProgress;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameBootstrapper : MonoInstaller
    {
        private Game _game;

        public override void InstallBindings()
        {
            Application.targetFrameRate = 60;

            Container.BindInterfacesTo<PlayfabTimeService>().AsSingle();

            BindModels();
            BindServices();
            BindStates();

            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
            
            Container.Bind<Game>().AsSingle();
        }
        
        private void BindModels()
        {
            Container.Bind<ClickerModel>().AsSingle();
        }
        
        private void BindServices()
        {
            Container.BindInterfacesTo<GameFactory>().AsSingle();
            Container.BindInterfacesTo<StaticDataService>().AsSingle();
            Container.BindInterfacesTo<WindowService>().AsSingle();
            Container.Bind<ITelegramService>().To(GetTelegramServiceImplementation()).AsSingle();
            Container.Bind<UserDataService>().AsSingle();
            Container.Bind<IAuthorizationService>().To<PlayfabAuthorizationService>().AsSingle();
            Container.Bind<IUserProgressService>().To<PlayfabUserProgressService>().AsSingle();
            Container.Bind<ILeaderboardService>().To<PlayfabLeaderboardService>().AsSingle();
        }

        private void BindStates()
        {
            Container.Bind<IExitableState>().To<BootstrapState>().AsSingle();
            Container.Bind<IExitableState>().To<UserAuthorizationState>().AsSingle();
            Container.Bind<IExitableState>().To<LoadProgressState>().AsSingle();
            Container.Bind<IExitableState>().To<LoadLevelState>().AsSingle();
            Container.Bind<IExitableState>().To<GameLoopState>().AsSingle();
        }

        public override void Start()
        {
            _game = Container.Resolve<Game>();
            _game.StateMachine.Enter<BootstrapState>();
        }

        private Type GetTelegramServiceImplementation() => 
            Application.isEditor ? 
                typeof(MockedTelegramService) : 
                typeof(TelegramService);
    }
}