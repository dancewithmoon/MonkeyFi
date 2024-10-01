using Base.Services;
using Base.States;
using MonkeyBusiness.Infrastructure.Factory;
using MonkeyBusiness.Logic.Hero;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadMonkeyBusinessState : IState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly IMonkeyBusinessFactory _factory;
        
        public IGameStateMachine StateMachine { get; set; }

        public LoadMonkeyBusinessState(SceneLoader sceneLoader, IMonkeyBusinessFactory factory)
        {
            _sceneLoader = sceneLoader;
            _factory = factory;
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
            StateMachine.Enter<MonkeyBusinessState>();
        }
    }
}