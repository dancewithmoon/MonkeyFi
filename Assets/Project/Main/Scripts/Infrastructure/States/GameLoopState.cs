using System;
using Base.States;
using Infrastructure.StaticData.Services;
using Models;
using Services.UserProgress;
using StaticData;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly ClickerModel _clickerModel;
        private readonly IUserProgressService _progressService;
        private readonly IStaticDataService _staticDataService;
        private ConfigStaticData _config;
        private DateTime _lastSaveTime;

        public IGameStateMachine StateMachine { get; set; }

        public GameLoopState(ClickerModel clickerModel, IUserProgressService progressService, IStaticDataService staticDataService)
        {
            _clickerModel = clickerModel;
            _progressService = progressService;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            _config = _staticDataService.GetConfig();
            _clickerModel.OnStateChangedEvent += UpdateProgress;
        }

        public void Exit()
        {
            _clickerModel.OnStateChangedEvent -= UpdateProgress;
        }

        private void UpdateProgress()
        {
            TimeSpan timePassed = DateTime.Now - _lastSaveTime;
            if (timePassed.TotalSeconds >= _config.SaveFrequencyInSeconds)
            {
                _progressService.SaveProgress();
                _lastSaveTime = DateTime.Now;
            }
        }
    }
}