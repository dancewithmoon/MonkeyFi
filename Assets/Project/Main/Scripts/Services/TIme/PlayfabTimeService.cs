using System;
using System.Threading.Tasks;
using Infrastructure.States;
using PlayFab.ClientModels;
using UnityEngine;
using Utils;

namespace Services.Time
{
    public class PlayfabTimeService : ITimeService, IPreloadedAfterAuthorization
    {
        private TimeSpan _timeOffset;
        public DateTime UtcNow => DateTime.UtcNow - _timeOffset;

        public async Task Preload()
        {
            GetTimeResult result = await PlayFabClientAsyncAPI.GetTime(new GetTimeRequest());
            _timeOffset = DateTime.UtcNow - result.Time;
            Debug.Log("Time synchronized. UTC Now: " + UtcNow);
        }
    }
}