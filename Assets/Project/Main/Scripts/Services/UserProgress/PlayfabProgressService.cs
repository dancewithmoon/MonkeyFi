﻿using System;
using System.Collections.Generic;
using Infrastructure.StaticData.Services;
using Models;
using PlayFab;
using PlayFab.ClientModels;
using StaticData;
using UnityEngine;

namespace Services.UserProgress
{
    public class PlayfabProgressService : IUserProgressService
    {
        private readonly UserDataService _userDataService;
        private readonly IStaticDataService _staticDataService;
        private readonly ClickerModel _clickerModel;
        private string _playfabId;
        private GetUserDataResult _rawProgress;

        public event Action OnProgressLoadedEvent;

        public PlayfabProgressService(UserDataService userDataService, IStaticDataService staticDataService, ClickerModel clickerModel)
        {
            _userDataService = userDataService;
            _staticDataService = staticDataService;
            _clickerModel = clickerModel;
        }
        
        public void LoadProgress()
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = _userDataService.Id.ToString(),
                CreateAccount = true
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnPlayfabError);
        }

        public void SaveProgress()
        {
            var request = new UpdateUserDataRequest
            {
                Data = GetProgressData()
            };
            PlayFabClientAPI.UpdateUserData(request, OnProgressSaved, OnPlayfabError);
        }

        private void OnLoginSuccess(LoginResult result)
        {
            _playfabId = result.PlayFabId;
            StartProgressLoading();
        }

        private void StartProgressLoading()
        {
            var request = new GetUserDataRequest()
            {
                PlayFabId = _playfabId,
                Keys = null
            };
            PlayFabClientAPI.GetUserData(request, OnProgressLoaded, OnPlayfabError);
        }

        private void OnProgressLoaded(GetUserDataResult result)
        {
            Debug.Log("Progress loaded: " + result);
            
            _rawProgress = result;

            ConfigStaticData config = _staticDataService.GetConfig();
            int clickerPoints = GetIntegerValue(ProgressKeys.ClickerPointsKey, 0);
            int maxEnergy = GetIntegerValue(ProgressKeys.MaxEnergyKey, config.DefaultMaxEnergy);
            int currentEnergy = GetIntegerValue(ProgressKeys.CurrentEnergyKey, maxEnergy);
            int energyRechargePerSecond = GetIntegerValue(ProgressKeys.EnergyRechargePerSecondKey, config.DefaultEnergyRechargePerSecond);
            
            _clickerModel.UpdateValues(clickerPoints, currentEnergy, maxEnergy, energyRechargePerSecond);
            
            OnProgressLoadedEvent?.Invoke();
        }

        private int GetIntegerValue(string key, int defaultValue)
        {
            if (_rawProgress.Data.TryGetValue(key, out UserDataRecord record))
                return int.Parse(record.Value);

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