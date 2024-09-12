using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.Services;
using Base.States;
using Infrastructure.Factory;
using Services;
using Services.Telegram;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly List<IPreloadedInLoadLevel> _toPreload;
        private readonly List<ICleanUp> _toCleanUp;
        private readonly IGameFactory _gameFactory;
        private readonly IWindowService _windowService;
        private readonly TelegramService _telegramService;

        private Task _preload;
        private string _currentLevel;
        
        public IGameStateMachine StateMachine { get; set; }

        public LoadLevelState(SceneLoader sceneLoader, List<IPreloadedInLoadLevel> toPreload, List<ICleanUp> toCleanUp,
            IGameFactory gameFactory, IWindowService windowService, TelegramService telegramService)
        {
            _sceneLoader = sceneLoader;
            _toPreload = toPreload;
            _toCleanUp = toCleanUp;
            _gameFactory = gameFactory;
            _windowService = windowService;
            _telegramService = telegramService;
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
            _telegramService.Initialize();
            await Task.WhenAll(_toPreload.Select(obj => obj.Preload()));
        }

        private async void OnLoaded()
        {
            await _preload;
            _gameFactory.CreateUIRoot();
            _gameFactory.CreateHudOverlay();
            _windowService.ShowWindow(WindowType.Clicker);
            StateMachine.Enter<GameLoopState>();
        }
    }
}