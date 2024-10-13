using System.Collections;
using Base.Services.CoroutineRunner;
using Base.States;
using Infrastructure.StaticData.Services;
using Models;
using Services;
using Services.Leaderboard;
using Services.UserProgress;
using StaticData;
using UnityEngine;

namespace Infrastructure.States
{
    public class MenuState : IState
    {
        private readonly ClickerModel _clickerModel;
        private readonly IUserProgressService _progressService;
        private readonly ILeaderboardService _leaderboardService;
        private readonly IStaticDataService _staticDataService;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IWindowService _windowService;
        private ConfigStaticData _config;
        private Coroutine _energyRechargeCoroutine;
        private Coroutine _progressSaveCoroutine;
        private Coroutine _statisticsUpdateCoroutine;

        public IGameStateMachine StateMachine { get; set; }

        public MenuState(ClickerModel clickerModel, IUserProgressService progressService,
            ILeaderboardService leaderboardService, IStaticDataService staticDataService,
            ICoroutineRunner coroutineRunner, IWindowService windowService)
        {
            _clickerModel = clickerModel;
            _progressService = progressService;
            _leaderboardService = leaderboardService;
            _staticDataService = staticDataService;
            _coroutineRunner = coroutineRunner;
            _windowService = windowService;
        }

        public void Enter()
        {
            _config = _staticDataService.GetConfig();
            _energyRechargeCoroutine = _coroutineRunner.StartCoroutine(EnergyRechargeCoroutine());
            _progressSaveCoroutine = _coroutineRunner.StartCoroutine(ProgressSaveCoroutine());
            _statisticsUpdateCoroutine = _coroutineRunner.StartCoroutine(StatisticsUpdateCoroutine());
        }

        public void Exit()
        {
            _coroutineRunner.StopCoroutine(_energyRechargeCoroutine);
            _coroutineRunner.StopCoroutine(_progressSaveCoroutine);
            _coroutineRunner.StopCoroutine(_statisticsUpdateCoroutine);
            
            _windowService.ClearWindows();
        }

        private IEnumerator EnergyRechargeCoroutine()
        {
            var waitForSecond = new WaitForSeconds(1f);
            while (_coroutineRunner != null)
            {
                if (_clickerModel.NeedEnergyRecharge)
                    _clickerModel.RechargeEnergy();
                
                yield return waitForSecond;
            }
        }
        
        private IEnumerator ProgressSaveCoroutine()
        {
            WaitForSeconds waitForSave = new WaitForSeconds(_config.SaveFrequencyInSeconds);
            while (_coroutineRunner != null)
            {
                yield return waitForSave;
                _progressService.SaveProgress();
            }
        }

        private IEnumerator StatisticsUpdateCoroutine()
        {
            WaitForSeconds waitForSave = new WaitForSeconds(_config.StatisticsUpdateFrequencyInSeconds);
            while (_coroutineRunner != null)
            {
                yield return waitForSave;
                _leaderboardService.UpdatePlayerStatistics();
            }
        }
    }
}