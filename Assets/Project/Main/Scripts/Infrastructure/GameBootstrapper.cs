using System;
using Base;
using Base.States;
using Infrastructure.Factory;
using Infrastructure.States;
using Infrastructure.StaticData.Services;
using Models;
using MonkeyBusiness.Infrastructure.Factory;
using Services;
using Services.Leaderboard;
using Services.Login;
using Services.Referral;
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
            Container.Bind<IMonkeyBusinessFactory>().To<MonkeyBusinessFactory>().AsSingle();
            Container.BindInterfacesTo<GameFactory>().AsSingle();
            Container.BindInterfacesTo<StaticDataService>().AsSingle();
            Container.BindInterfacesTo<WindowService>().AsSingle();
            Container.BindInterfacesTo(GetTelegramServiceImplementation()).AsSingle();
            Container.Bind<IReferralService>().To<PlayfabReferralService>().AsSingle();
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
            Container.Bind<IExitableState>().To<LoadMenuState>().AsSingle();
            Container.Bind<IExitableState>().To<MenuState>().AsSingle();
            Container.Bind<IExitableState>().To<LoadMonkeyBusinessState>().AsSingle();
            Container.Bind<IExitableState>().To<MonkeyBusinessState>().AsSingle();
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