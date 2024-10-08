﻿using System;
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

        private int _clickerPoints;
        private int _maxEnergy;
        private int _currentEnergy;
        private int _energyRechargePerSecond;

        public event Action OnProgressLoadedEvent;

        public PlayfabUserProgressService(IStaticDataService staticDataService, ITimeService timeService, ClickerModel clickerModel)
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
            Dictionary<string, string> progressData = GetProgressData();
            if(progressData.Count == 0)
                return;
            
            var request = new UpdateUserDataRequest
            {
                Data = progressData
            };
            PlayFabClientAPI.UpdateUserData(request, OnProgressSaved, OnPlayfabError);
        }
        
        private void OnProgressLoaded(GetUserDataResult result)
        {
            Debug.Log("Progress loaded: " + result);
            
            _rawProgress = result;

            ConfigStaticData config = _staticDataService.GetConfig();
            _clickerPoints = GetIntegerValue(ProgressKeys.ClickerPointsKey, 0);
            _maxEnergy = GetIntegerValue(ProgressKeys.MaxEnergyKey, config.DefaultMaxEnergy);
            _energyRechargePerSecond = GetIntegerValue(ProgressKeys.EnergyRechargePerSecondKey, config.DefaultEnergyRechargePerSecond);
            _currentEnergy = GetEnergyValue(_maxEnergy);
            
            _clickerModel.UpdateValues(_clickerPoints, _currentEnergy, _maxEnergy, _energyRechargePerSecond, _lastEnergyUpdateTime);
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

        private void OnProgressSaved(UpdateUserDataResult result) => 
            Debug.Log("Progress saved: " + result);
        
        private void OnPlayfabError(PlayFabError error) => 
            Debug.LogError("Playfab error: " + error);
    }
}