using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Infrastructure.States;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace Services.Time
{
    public class PlayfabTimeService : ITimeService, IPreloadedAfterAuthorization
    {
        private GetTimeResult _result;
        private TimeSpan _timeOffset;

        public async Task Preload()
        {
            var request = new GetTimeRequest();
            PlayFabClientAPI.GetTime(request, OnTimeGet, OnError);
            await UniTask.WaitUntil(() => _result != null);
            _timeOffset = DateTime.UtcNow - _result.Time;
            _result = null;
        }

        public DateTime UtcNow => DateTime.UtcNow - _timeOffset;

        private void OnTimeGet(GetTimeResult result) => _result = result;

        private void OnError(PlayFabError error) => 
            Debug.LogError("Get time error: " + error);
    }
}