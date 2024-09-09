using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.Services;
using Base.States;

namespace Main.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly List<IPreloadedInLoadLevel> _toPreload;
        private readonly List<ICleanUp> _toCleanUp;

        private Task _preload;
        private string _currentLevel;
        
        public IGameStateMachine StateMachine { get; set; }

        public LoadLevelState(SceneLoader sceneLoader, List<IPreloadedInLoadLevel> toPreload, List<ICleanUp> toCleanUp)
        {
            _sceneLoader = sceneLoader;
            _toPreload = toPreload;
            _toCleanUp = toCleanUp;
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
            StateMachine.Enter<GameLoopState>();
        }
    }
}