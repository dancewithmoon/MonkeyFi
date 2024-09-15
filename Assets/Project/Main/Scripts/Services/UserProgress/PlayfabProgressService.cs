using System;
using Infrastructure.StaticData.Services;
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
        private string _playfabId;
        private GetUserDataResult _rawProgress;

        public int ClickerPoints { get; private set; }
        public int CurrentEnergy { get; private set; }
        public int MaxEnergy { get; private set; }
        public int EnergyRechargePerSecond { get; private set; }
        
        public event Action OnProgressLoadedEvent;

        public PlayfabProgressService(UserDataService userDataService, IStaticDataService staticDataService)
        {
            _userDataService = userDataService;
            _staticDataService = staticDataService;
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
            _rawProgress = result;

            ConfigStaticData config = _staticDataService.GetConfig();
            MaxEnergy = GetIntegerValue(nameof(MaxEnergy), config.DefaultMaxEnergy);
            CurrentEnergy = GetIntegerValue(nameof(CurrentEnergy), MaxEnergy);
            ClickerPoints = GetIntegerValue(nameof(ClickerPoints), 0);
            EnergyRechargePerSecond = GetIntegerValue(nameof(EnergyRechargePerSecond), config.DefaultEnergyRechargePerSecond);
            
            OnProgressLoadedEvent?.Invoke();
        }

        private int GetIntegerValue(string key, int defaultValue)
        {
            if (_rawProgress.Data.TryGetValue(key, out UserDataRecord record))
                return int.Parse(record.Value);

            return defaultValue;
        }

        private void OnPlayfabError(PlayFabError error) => 
            Debug.LogError("Playfab error: " + error);
    }
}