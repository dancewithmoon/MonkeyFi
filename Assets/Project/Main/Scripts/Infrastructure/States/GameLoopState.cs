using System.Collections;
using Base.Services.CoroutineRunner;
using Base.States;
using Infrastructure.StaticData.Services;
using Models;
using Services.UserProgress;
using StaticData;
using UnityEngine;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly ClickerModel _clickerModel;
        private readonly IUserProgressService _progressService;
        private readonly IStaticDataService _staticDataService;
        private readonly ICoroutineRunner _coroutineRunner;
        private ConfigStaticData _config;
        private Coroutine _energyRechargeCoroutine;
        private Coroutine _progressSaveCoroutine;

        public IGameStateMachine StateMachine { get; set; }

        public GameLoopState(ClickerModel clickerModel, IUserProgressService progressService, IStaticDataService staticDataService, ICoroutineRunner coroutineRunner)
        {
            _clickerModel = clickerModel;
            _progressService = progressService;
            _staticDataService = staticDataService;
            _coroutineRunner = coroutineRunner;
        }

        public void Enter()
        {
            _config = _staticDataService.GetConfig();
            _energyRechargeCoroutine = _coroutineRunner.StartCoroutine(EnergyRechargeCoroutine());
            _progressSaveCoroutine = _coroutineRunner.StartCoroutine(ProgressSaveCoroutine());
        }

        public void Exit()
        {
            _coroutineRunner.StopCoroutine(_energyRechargeCoroutine);
            _coroutineRunner.StopCoroutine(_progressSaveCoroutine);
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
    }
}