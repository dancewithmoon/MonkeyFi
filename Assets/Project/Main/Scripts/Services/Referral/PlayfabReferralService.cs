using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlayFab.ClientModels;
using Services.Config;
using Services.Login;
using UnityEngine;
using Utils;

namespace Services.Referral
{
    public class PlayfabReferralService : IReferralService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IConfigProvider _configProvider;
        private readonly IShareService _shareService;

        private const string AddReferralFunctionName = "AddReferral";
        private const string GetReferralProfilesFunctionName = "GetReferralProfiles";

        public List<ReferralModel> Referrals { get; private set; }

        public PlayfabReferralService(IAuthorizationService authorizationService, IConfigProvider configProvider, IShareService shareService)
        {
            _authorizationService = authorizationService;
            _configProvider = configProvider;
            _shareService = shareService;
        }

        public async void ConnectToReferrer(string referralCode)
        {
            string referrerId = await GetReferrerPlayfabId(referralCode);
            var request = new ExecuteCloudScriptRequest 
            { 
                FunctionName = AddReferralFunctionName,
                GeneratePlayStreamEvent = true, 
                FunctionParameter = new { referrerId }
            };
            ExecuteCloudScriptResult rawResult = await PlayFabClientAsyncAPI.ExecuteCloudScript(request);
            
            var result = JsonUtility.FromJson<AddReferralResult>(rawResult.FunctionResult.ToString());
            if (result.success)
                Debug.Log($"Referrer {result.referrer} successfully got the new referral");
        }

        public async Task LoadReferrals()
        {
            var request = new ExecuteCloudScriptRequest
            {
                FunctionName = GetReferralProfilesFunctionName,
                GeneratePlayStreamEvent = true
            };
            ExecuteCloudScriptResult rawResult = await PlayFabClientAsyncAPI.ExecuteCloudScript(request);
            
            var result = JsonUtility.FromJson<GetReferralProfilesResult>(rawResult.FunctionResult.ToString());
            Referrals = result.profiles.Select(profile => new ReferralModel(profile.DisplayName)).ToList();
        }

        public void InviteFriends()
        {
            string shareUrl = _configProvider.Config.ShareUrl + _authorizationService.UserUniqueId;
            _shareService.Share(_configProvider.Config.ShareMessage, shareUrl);
        }

        private async Task<string> GetReferrerPlayfabId(string referralCode)
        {
            var getAccountInfoRequest = new GetAccountInfoRequest { Username = referralCode };
            GetAccountInfoResult getAccountInfoResult = await PlayFabClientAsyncAPI.GetAccountInfo(getAccountInfoRequest);
            return getAccountInfoResult.AccountInfo.PlayFabId;
        }

        [Serializable]
        private class AddReferralResult
        {
            public bool success;
            public string referrer;
        }

        [Serializable]
        private class GetReferralProfilesResult
        {
            public List<PlayerProfileModel> profiles;
        }
    }
}