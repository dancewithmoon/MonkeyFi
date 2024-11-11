using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;
using PlayFab.ClientModels;
using Services.Library.Config;
using Services.Quests;
using Services.Time;
using UnityEngine;
using Utils;

namespace Services.UserProgress
{
    public class PlayfabUserProgressService : IUserProgressService
    {
        private readonly IConfigProvider _configProvider;
        private readonly IQuestsService _questsService;
        private readonly ITimeService _timeService;
        private readonly ClickerModel _clickerModel;
        private GetUserDataResult _rawProgress;
        private DateTime _lastEnergyUpdateTime;

        private int _clickerPoints;
        private int _maxEnergy;
        private int _currentEnergy;
        private int _energyRechargePerSecond;
        
        private List<int> _completedConditionIds;
        private List<int> _completedQuestIds;

        public PlayfabUserProgressService(IConfigProvider configProvider, IQuestsService questsService, ITimeService timeService, ClickerModel clickerModel)
        {
            _configProvider = configProvider;
            _questsService = questsService;
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
            _completedConditionIds = GetIntegerList(ProgressKeys.CompletedConditions);
            _completedQuestIds = GetIntegerList(ProgressKeys.CompletedQuests);
            
            _clickerModel.UpdateValues(_clickerPoints, _currentEnergy, _maxEnergy, _energyRechargePerSecond, _lastEnergyUpdateTime);
            _clickerModel.RechargeEnergy();

            _questsService.UpdateProgress(_completedConditionIds, _completedQuestIds);
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
        
        private List<int> GetIntegerList(string key)
        {
            if (_rawProgress.Data.TryGetValue(key, out UserDataRecord record))
                return JsonConvert.DeserializeObject<List<int>>(record.Value);

            return null;
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

            List<int> completedConditionIds = _questsService.CompletedConditions.Select(condition => condition.Data.Id).ToList();
            if (completedConditionIds.Count > 0 && (_completedConditionIds == null || _completedConditionIds.Count != completedConditionIds.Count))
            {
                _completedConditionIds = completedConditionIds;
                result[ProgressKeys.CompletedConditions] = JsonConvert.SerializeObject(_completedConditionIds);
            }
            
            List<int> completedQuestIds = _questsService.CompletedQuests.Select(quest => quest.Data.Id).ToList();
            if (completedQuestIds.Count > 0 && (_completedQuestIds == null || _completedQuestIds.Count != completedQuestIds.Count))
            {
                _completedQuestIds = completedQuestIds;
                result[ProgressKeys.CompletedQuests] = JsonConvert.SerializeObject(_completedQuestIds);
            }

            return result;
        }
    }
}