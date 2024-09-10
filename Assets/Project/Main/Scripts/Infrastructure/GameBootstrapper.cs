﻿using Base;
using Base.States;
using Main.Infrastructure.Factory;
using Main.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Main.Infrastructure
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
    }
}