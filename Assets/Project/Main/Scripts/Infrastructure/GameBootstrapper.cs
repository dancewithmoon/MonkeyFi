using System;
using Base;
using Base.States;
using Infrastructure.Factory;
using Infrastructure.States;
using Infrastructure.StaticData.Services;
using Services;
using Services.Telegram;
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

            BindServices();
            BindStates();

            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
            
            Container.Bind<Game>().AsSingle();
        }
        
        private void BindServices()
        {
            Container.BindInterfacesTo<GameFactory>().AsSingle();
            Container.BindInterfacesTo<StaticDataService>().AsSingle();
            Container.BindInterfacesTo<WindowService>().AsSingle();
            Container.Bind<ITelegramService>().To(GetTelegramServiceImplementation()).AsSingle();
            Container.Bind<UserDataService>().AsSingle();
            Container.Bind<UserProgressService>().AsSingle();
        }

        private void BindStates()
        {
            Container.Bind<IExitableState>().To<BootstrapState>().AsSingle();
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