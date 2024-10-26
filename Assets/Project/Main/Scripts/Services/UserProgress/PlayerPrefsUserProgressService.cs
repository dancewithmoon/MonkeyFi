using System;
using System.Threading.Tasks;
using Models;
using Services.Config;
using Services.Time;
using StaticData;
using UnityEngine;

namespace Services.UserProgress
{
    public class PlayerPrefsUserProgressService : IUserProgressService
    {
        private const string LastEnergyUpdateTimeKey = "LastEnergyUpdateTime";
        
        private readonly IConfigService _configService;
        private readonly ITimeService _timeService;
        private readonly ClickerModel _clickerModel;

        public PlayerPrefsUserProgressService(IConfigService configService, ITimeService timeService, ClickerModel clickerModel)
        {
            _configService = configService;
            _timeService = timeService;
            _clickerModel = clickerModel;
        }

        public Task LoadProgress()
        {
            int clickerPoints = PlayerPrefs.GetInt(ProgressKeys.ClickerPointsKey, 0);
            int maxEnergy = PlayerPrefs.GetInt(ProgressKeys.MaxEnergyKey, _configService.Config.DefaultMaxEnergy);
            int currentEnergy = PlayerPrefs.GetInt(ProgressKeys.CurrentEnergyKey, maxEnergy);
            int energyRechargePerSecond = PlayerPrefs.GetInt(ProgressKeys.EnergyRechargePerSecondKey, _configService.Config.DefaultEnergyRechargePerSecond);
            
            string rawLastEnergyUpdateTime = PlayerPrefs.GetString(LastEnergyUpdateTimeKey, string.Empty);
            
            DateTime lastEnergyUpdateTime = string.IsNullOrEmpty(rawLastEnergyUpdateTime) ? 
                _timeService.UtcNow : 
                DateTime.Parse(rawLastEnergyUpdateTime);
            
            _clickerModel.UpdateValues(clickerPoints, currentEnergy, maxEnergy, energyRechargePerSecond, lastEnergyUpdateTime);

            return Task.CompletedTask;
        }

        public void SaveProgress()
        {
            PlayerPrefs.SetInt(ProgressKeys.ClickerPointsKey, _clickerModel.Points);
            PlayerPrefs.SetInt(ProgressKeys.MaxEnergyKey, _clickerModel.MaxEnergy);
            PlayerPrefs.SetInt(ProgressKeys.CurrentEnergyKey, _clickerModel.CurrentEnergy);
            PlayerPrefs.SetInt(ProgressKeys.EnergyRechargePerSecondKey, _clickerModel.EnergyRechargePerSecond);
            PlayerPrefs.SetString(LastEnergyUpdateTimeKey, _timeService.UtcNow.ToString());
        }
    }
}