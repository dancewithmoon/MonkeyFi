using PlayFab.ClientModels;
using UnityEngine;
using Utils;

namespace Services.Referral
{
    public class PlayfabReferralService : IReferralService
    {
        public async void GetReferrer(string referralCode)
        {
            Debug.Log("REF CODE: " + referralCode);
            var request = new GetAccountInfoRequest { Username = referralCode };
            GetAccountInfoResult result = await PlayFabClientAsyncAPI.GetAccountInfo(request);
            Debug.LogError("SUCCESS: " + result.AccountInfo.PlayFabId);
        }
    }
}