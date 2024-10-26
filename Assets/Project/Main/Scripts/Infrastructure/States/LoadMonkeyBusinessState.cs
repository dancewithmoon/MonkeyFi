using Base.Services;
using Base.States;
using MonkeyBusiness.Infrastructure.Factory;
using MonkeyBusiness.Logic.Hero;
using Services;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadMonkeyBusinessState : IState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly IMonkeyBusinessFactory _factory;
        private readonly IWindowService _windowService;

        public IGameStateMachine StateMachine { get; set; }

        public LoadMonkeyBusinessState(SceneLoader sceneLoader, IMonkeyBusinessFactory factory, IWindowService windowService)
        {
            _sceneLoader = sceneLoader;
            _factory = factory;
            _windowService = windowService;
        }

        public void Enter()
        {
            _sceneLoader.Load(Scenes.MonkeyBusiness, OnLoaded);
        }

        public void Exit()
        {

        }

        private async void OnLoaded()
        {
            GameObject hero = await _factory.CreateHero(Vector3.zero);
            hero.GetComponent<HeroStateMachine>().Enter(HeroStateType.Idle);
            ShowHudOverlay();
            StateMachine.Enter<MonkeyBusinessState>();
        }

        private void ShowHudOverlay()
        {
            _windowService.ShowHudOverlay();
            _windowService.Hud.SetBottomPanelActive(false);
            _windowService.Hud.SetBackButtonActive(true);
        }
    }
}