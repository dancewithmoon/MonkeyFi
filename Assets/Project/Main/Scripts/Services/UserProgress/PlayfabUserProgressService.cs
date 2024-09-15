using System;
using System.Collections.Generic;
using Infrastructure.StaticData.Services;
using Models;
using PlayFab;
using PlayFab.ClientModels;
using Services.Time;
using StaticData;
using UnityEngine;

namespace Services.UserProgress
{
    public class PlayfabUserProgressService : IUserProgressService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ITimeService _timeService;
        private readonly ClickerModel _clickerModel;
        private GetUserDataResult _rawProgress;
        private DateTime _lastEnergyUpdateTime;

        public event Action OnProgressLoadedEvent;

        public PlayfabUserProgressService(UserDataService userDataService, IStaticDataService staticDataService, ITimeService timeService, ClickerModel clickerModel)
        {
            _staticDataService = staticDataService;
            _timeService = timeService;
            _clickerModel = clickerModel;
        }
        
        public void LoadProgress()
        {
            var request = new GetUserDataRequest();
            PlayFabClientAPI.GetUserData(request, OnProgressLoaded, OnPlayfabError);
        }

        public void SaveProgress()
        {
            var request = new UpdateUserDataRequest
            {
                Data = GetProgressData()
            };
            PlayFabClientAPI.UpdateUserData(request, OnProgressSaved, OnPlayfabError);
        }
        
        private void OnProgressLoaded(GetUserDataResult result)
        {
            Debug.Log("Progress loaded: " + result);
            
            _rawProgress = result;

            ConfigStaticData config = _staticDataService.GetConfig();
            int clickerPoints = GetIntegerValue(ProgressKeys.ClickerPointsKey, 0);
            int maxEnergy = GetIntegerValue(ProgressKeys.MaxEnergyKey, config.DefaultMaxEnergy);
            int energyRechargePerSecond = GetIntegerValue(ProgressKeys.EnergyRechargePerSecondKey, config.DefaultEnergyRechargePerSecond);
            int currentEnergy = GetEnergyValue(maxEnergy);
            
            _clickerModel.UpdateValues(clickerPoints, currentEnergy, maxEnergy, energyRechargePerSecond, _lastEnergyUpdateTime);
            _clickerModel.RechargeEnergy();
            OnProgressLoadedEvent?.Invoke();
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
            return new Dictionary<string, string>
            {
                { ProgressKeys.ClickerPointsKey, _clickerModel.Points.ToString() },
                { ProgressKeys.CurrentEnergyKey, _clickerModel.CurrentEnergy.ToString() },
                { ProgressKeys.MaxEnergyKey, _clickerModel.MaxEnergy.ToString() },
                { ProgressKeys.EnergyRechargePerSecondKey, _clickerModel.EnergyRechargePerSecond.ToString() }
            };
        }

        private void OnProgressSaved(UpdateUserDataResult result) => 
            Debug.Log("Progress saved: " + result);
        
        private void OnPlayfabError(PlayFabError error) => 
            Debug.LogError("Playfab error: " + error);
    }
}