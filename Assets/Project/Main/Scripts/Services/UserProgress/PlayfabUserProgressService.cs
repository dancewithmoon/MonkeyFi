using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using PlayFab.ClientModels;
using Services.Config;
using Services.Time;
using UnityEngine;
using Utils;

namespace Services.UserProgress
{
    public class PlayfabUserProgressService : IUserProgressService
    {
        private readonly IConfigProvider _configProvider;
        private readonly ITimeService _timeService;
        private readonly ClickerModel _clickerModel;
        private GetUserDataResult _rawProgress;
        private DateTime _lastEnergyUpdateTime;

        private int _clickerPoints;
        private int _maxEnergy;
        private int _currentEnergy;
        private int _energyRechargePerSecond;

        public PlayfabUserProgressService(IConfigProvider configProvider, ITimeService timeService, ClickerModel clickerModel)
        {
            _configProvider = configProvider;
            _timeService = timeService;
            _clickerModel = clickerModel;
        }
        
        public async Task LoadProgress()
        {
            GetUserDataResult result = await PlayFabClientAsyncAPI.GetUserData(new GetUserDataRequest());
            
            Debug.Log("Progress loaded: " + result.ToJson());
            
            _rawProgress = result;
            
            _clickerPoints = GetIntegerValue(ProgressKeys.ClickerPointsKey, 0);
            _maxEnergy = GetIntegerValue(ProgressKeys.MaxEnergyKey, _configProvider.Config.DefaultMaxEnergy);
            _energyRechargePerSecond = GetIntegerValue(ProgressKeys.EnergyRechargePerSecondKey, _configProvider.Config.DefaultEnergyRechargePerSecond);
            _currentEnergy = GetEnergyValue(_maxEnergy);
            
            _clickerModel.UpdateValues(_clickerPoints, _currentEnergy, _maxEnergy, _energyRechargePerSecond, _lastEnergyUpdateTime);
            _clickerModel.RechargeEnergy();
        }

        public async void SaveProgress()
        {
            Dictionary<string, string> progressData = GetProgressData();
            if(progressData.Count == 0)
                return;
            
            var request = new UpdateUserDataRequest { Data = progressData };
            UpdateUserDataResult result = await PlayFabClientAsyncAPI.UpdateUserData(request);
            Debug.Log("Progress saved: " + result);
        }

        private int GetIntegerValue(string key, int defaultValue)
        {
            if (_rawProgress.Data.TryGetValue(key, out UserDataRecord record))
                return int.Parse(record.Value);

            return defaultValue;
        }

        private int GetEnergyValue(int defaultValue)
        {
            if (_rawProgress.Data.TryGetValue(ProgressKeys.CurrentEnergyKey, out UserDataRecord currentEnergyRecord))
            {
                _lastEnergyUpdateTime = currentEnergyRecord.LastUpdated;
                return int.Parse(currentEnergyRecord.Value);
            }

            _lastEnergyUpdateTime = _timeService.UtcNow;
            return defaultValue;
        }
        
        private Dictionary<string,string> GetProgressData()
        {
            var result = new Dictionary<string, string>();
            
            if (_clickerPoints != _clickerModel.Points)
            {
                _clickerPoints = _clickerModel.Points;
                result[ProgressKeys.ClickerPointsKey] = _clickerPoints.ToString();
            }
            
            if (_currentEnergy != _clickerModel.CurrentEnergy)
            {
                _currentEnergy = _clickerModel.CurrentEnergy;
                result[ProgressKeys.CurrentEnergyKey] = _currentEnergy.ToString();
            }
            
            if (_maxEnergy != _clickerModel.MaxEnergy)
            {
                _maxEnergy = _clickerModel.MaxEnergy;
                result[ProgressKeys.MaxEnergyKey] = _maxEnergy.ToString();
            }
            
            if (_energyRechargePerSecond != _clickerModel.EnergyRechargePerSecond)
            {
                _energyRechargePerSecond = _clickerModel.EnergyRechargePerSecond;
                result[ProgressKeys.EnergyRechargePerSecondKey] = _energyRechargePerSecond.ToString();
            }

            return result;
        }
    }
}