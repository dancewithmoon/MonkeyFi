using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.Services;
using Base.States;
using Infrastructure.Factory;
using Services;
using Services.TonWallet;
using UI.Windows;

namespace Infrastructure.States
{
    public class LoadMenuState : IState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly List<IPreloadedInLoadMenu> _toPreload;
        private readonly List<ICleanUp> _toCleanUp;
        private readonly IGameFactory _gameFactory;
        private readonly IWindowService _windowService;
        private readonly TonWalletService _tonWalletService;

        private Task _preload;

        public IGameStateMachine StateMachine { get; set; }

        public LoadMenuState(SceneLoader sceneLoader, List<IPreloadedInLoadMenu> toPreload, List<ICleanUp> toCleanUp,
            IGameFactory gameFactory, IWindowService windowService, TonWalletService tonWalletService)
        {
            _sceneLoader = sceneLoader;
            _toPreload = toPreload;
            _toCleanUp = toCleanUp;
            _gameFactory = gameFactory;
            _windowService = windowService;
            _tonWalletService = tonWalletService;
        }

        public void Enter()
        {
            _toCleanUp.ForEach(obj => obj.CleanUp());
            _preload = Preload();

            _sceneLoader.Load(Scenes.MainScene, OnLoaded);
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
            await _tonWalletService.Initialize();
            _gameFactory.CreateUIRoot();
            ShowHudOverlay();
            _windowService.ShowWindow(WindowType.Games);
            StateMachine.Enter<MenuState>();
        }

        private void ShowHudOverlay()
        {
            _windowService.ShowHudOverlay();
            _windowService.Hud.SetBottomPanelActive(true);
            _windowService.Hud.SetBackButtonActive(false);
        }
    }
}