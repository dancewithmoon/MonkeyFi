using System;
using Infrastructure.StaticData.Services;
using Models;
using StaticData;
using UnityEngine;

namespace Services.UserProgress
{
    public class PlayerPrefsUserProgressService : IUserProgressService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ClickerModel _clickerModel;

        public event Action OnProgressLoadedEvent;

        public PlayerPrefsUserProgressService(IStaticDataService staticDataService, ClickerModel clickerModel)
        {
            _staticDataService = staticDataService;
            _clickerModel = clickerModel;
        }

        public void LoadProgress()
        {
            ConfigStaticData config = _staticDataService.GetConfig();
            
            int clickerPoints = PlayerPrefs.GetInt(ProgressKeys.ClickerPointsKey, 0);
            int maxEnergy = PlayerPrefs.GetInt(ProgressKeys.MaxEnergyKey, config.DefaultMaxEnergy);
            int currentEnergy = PlayerPrefs.GetInt(ProgressKeys.CurrentEnergyKey, maxEnergy);
            int energyRechargePerSecond = PlayerPrefs.GetInt(ProgressKeys.EnergyRechargePerSecondKey, config.DefaultEnergyRechargePerSecond);

            _clickerModel.UpdateValues(clickerPoints, currentEnergy, maxEnergy, energyRechargePerSecond);
            
            OnProgressLoadedEvent?.Invoke();
        }

        public void SaveProgress()
        {
            PlayerPrefs.SetInt(ProgressKeys.ClickerPointsKey, _clickerModel.Points);
            PlayerPrefs.SetInt(ProgressKeys.MaxEnergyKey, _clickerModel.MaxEnergy);
            PlayerPrefs.SetInt(ProgressKeys.CurrentEnergyKey, _clickerModel.CurrentEnergy);
            PlayerPrefs.SetInt(ProgressKeys.EnergyRechargePerSecondKey, _clickerModel.EnergyRechargePerSecond);
        }
    }
}