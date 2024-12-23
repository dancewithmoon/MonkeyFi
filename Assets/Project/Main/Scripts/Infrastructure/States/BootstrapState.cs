﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.Services;
using Base.States;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly List<IPreloadedInBootstrap> _toPreload;

        public IGameStateMachine StateMachine { get; set; }

        public BootstrapState(SceneLoader sceneLoader, List<IPreloadedInBootstrap> toPreload)
        {
            _sceneLoader = sceneLoader;
            _toPreload = toPreload;
        }

        public async void Enter()
        {
            await Task.WhenAll(_toPreload.Select(obj => obj.Preload()));
            _sceneLoader.Load(Scenes.InitialScene, EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            StateMachine.Enter<UserAuthorizationState>();
        }
    }
}