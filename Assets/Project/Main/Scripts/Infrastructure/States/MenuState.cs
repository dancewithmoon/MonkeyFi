using System.Collections;
using Base.Services.CoroutineRunner;
using Base.States;
using Models;
using Services;
using Services.Config;
using Services.Leaderboard;
using Services.UserProgress;
using UnityEngine;

namespace Infrastructure.States
{
    public class MenuState : IState
    {
        private readonly ClickerModel _clickerModel;
        private readonly IUserProgressService _progressService;
        private readonly ILeaderboardService _leaderboardService;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IWindowService _windowService;
        private readonly IConfigService _configService;
        private Coroutine _energyRechargeCoroutine;
        private Coroutine _progressSaveCoroutine;
        private Coroutine _statisticsUpdateCoroutine;

        public IGameStateMachine StateMachine { get; set; }

        public MenuState(ClickerModel clickerModel, IUserProgressService progressService,
            ILeaderboardService leaderboardService, ICoroutineRunner coroutineRunner,
            IWindowService windowService, IConfigService configService)
        {
            _clickerModel = clickerModel;
            _progressService = progressService;
            _leaderboardService = leaderboardService;
            _coroutineRunner = coroutineRunner;
            _windowService = windowService;
            _configService = configService;
        }

        public void Enter()
        {
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
            WaitForSeconds waitForSave = new WaitForSeconds(_configService.Config.SaveFrequencyInSeconds);
            while (_coroutineRunner != null)
            {
                yield return waitForSave;
                _progressService.SaveProgress();
            }
        }

        private IEnumerator StatisticsUpdateCoroutine()
        {
            WaitForSeconds waitForSave = new WaitForSeconds(_configService.Config.StatisticsUpdateFrequencyInSeconds);
            while (_coroutineRunner != null)
            {
                yield return waitForSave;
                _leaderboardService.UpdatePlayerStatistics();
            }
        }
    }
}