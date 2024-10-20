using System.Threading.Tasks;
using PlayFab.ClientModels;
using UnityEngine;
using Utils;

namespace Services.Referral
{
    public class PlayfabReferralService : IReferralService
    {
        public async void ConnectToReferrer(string referralCode)
        {
            string referrerId = await GetReferrerPlayfabId(referralCode);
            var request = new ExecuteCloudScriptRequest { 
                FunctionName = "AddReferral",
                GeneratePlayStreamEvent = true, 
                FunctionParameter = new { referrerId }
            };

            ExecuteCloudScriptResult result = await PlayFabClientAsyncAPI.ExecuteCloudScript(request);
            if (result.FunctionResult is PlayFab.Json.JsonObject functionResult
                && functionResult.ContainsKey("success") && (bool)functionResult["success"])
            {
                Debug.Log("Referrer successfully got the new referral");
            }
        }

        private async Task<string> GetReferrerPlayfabId(string referralCode)
        {
            var getAccountInfoRequest = new GetAccountInfoRequest { Username = referralCode };
            GetAccountInfoResult getAccountInfoResult = await PlayFabClientAsyncAPI.GetAccountInfo(getAccountInfoRequest);
            return getAccountInfoResult.AccountInfo.PlayFabId;
        }
    }
}