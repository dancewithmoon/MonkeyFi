using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.Services;
using Base.States;
using Infrastructure.Factory;
using Services;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly List<IPreloadedInLoadLevel> _toPreload;
        private readonly List<ICleanUp> _toCleanUp;
        private readonly IGameFactory _gameFactory;
        private readonly IWindowService _windowService;

        private Task _preload;
        private string _currentLevel;
        
        public IGameStateMachine StateMachine { get; set; }

        public LoadLevelState(SceneLoader sceneLoader, List<IPreloadedInLoadLevel> toPreload, List<ICleanUp> toCleanUp,
            IGameFactory gameFactory, IWindowService windowService)
        {
            _sceneLoader = sceneLoader;
            _toPreload = toPreload;
            _toCleanUp = toCleanUp;
            _gameFactory = gameFactory;
            _windowService = windowService;
        }

        public void Enter(string sceneName)
        {
            _currentLevel = sceneName;
            _toCleanUp.ForEach(obj => obj.CleanUp());
            _preload = Preload();

            _sceneLoader.Load(_currentLevel, OnLoaded);
        }

        public void Exit()
        {
        }

        private async Task Preload()
        {
            await Task.WhenAll(_toPreload.Select(obj => obj.Preload()));
        }

        private async void OnLoaded()
        {
            await _preload;
            _gameFactory.CreateUIRoot();
            _gameFactory.CreateHudOverlay();
            _windowService.ShowWindow(WindowType.Games);
            StateMachine.Enter<GameLoopState>();
        }
    }
}