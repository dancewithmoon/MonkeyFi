using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.SharedModels;

namespace Utils
{
    public static class PlayFabClientAsyncAPI
    {
        public static async Task<LoginResult> LoginWithCustomID(LoginWithCustomIDRequest request) => 
            await PlayFabApiCall<LoginWithCustomIDRequest, LoginResult>(request, PlayFabClientAPI.LoginWithCustomID);

        public static async Task<GetAccountInfoResult> GetAccountInfo(GetAccountInfoRequest request) => 
            await PlayFabApiCall<GetAccountInfoRequest, GetAccountInfoResult>(request, PlayFabClientAPI.GetAccountInfo);

        public static async Task<GetUserDataResult> GetUserReadonlyData(GetUserDataRequest request) => 
            await PlayFabApiCall<GetUserDataRequest, GetUserDataResult>(request, PlayFabClientAPI.GetUserReadOnlyData);
        
        public static async Task<GetUserDataResult> GetUserData(GetUserDataRequest request) => 
            await PlayFabApiCall<GetUserDataRequest, GetUserDataResult>(request, PlayFabClientAPI.GetUserData);
        
        public static async Task<GetPlayerProfileResult> GetPlayerProfile(GetPlayerProfileRequest request) =>
            await PlayFabApiCall<GetPlayerProfileRequest, GetPlayerProfileResult>(request, PlayFabClientAPI.GetPlayerProfile);

        public static async Task<UpdateUserDataResult> UpdateUserData(UpdateUserDataRequest request) => 
            await PlayFabApiCall<UpdateUserDataRequest, UpdateUserDataResult>(request, PlayFabClientAPI.UpdateUserData);

        public static async Task<GetLeaderboardResult> GetLeaderboard(GetLeaderboardRequest request) => 
            await PlayFabApiCall<GetLeaderboardRequest, GetLeaderboardResult>(request, PlayFabClientAPI.GetLeaderboard);
        
        public static async Task<UpdatePlayerStatisticsResult> UpdatePlayerStatistics(UpdatePlayerStatisticsRequest request) => 
            await PlayFabApiCall<UpdatePlayerStatisticsRequest, UpdatePlayerStatisticsResult>(request, PlayFabClientAPI.UpdatePlayerStatistics);

        public static async Task<UpdateUserTitleDisplayNameResult> UpdateUserTitleDisplayName(UpdateUserTitleDisplayNameRequest request) =>
            await PlayFabApiCall<UpdateUserTitleDisplayNameRequest, UpdateUserTitleDisplayNameResult>(request, PlayFabClientAPI.UpdateUserTitleDisplayName);

        public static async Task<AddUsernamePasswordResult> AddUsernamePassword(AddUsernamePasswordRequest request) =>
            await PlayFabApiCall<AddUsernamePasswordRequest, AddUsernamePasswordResult>(request, PlayFabClientAPI.AddUsernamePassword);

        public static async Task<ExecuteCloudScriptResult> ExecuteCloudScript(ExecuteCloudScriptRequest request) => 
            await PlayFabApiCall<ExecuteCloudScriptRequest, ExecuteCloudScriptResult>(request, PlayFabClientAPI.ExecuteCloudScript);

        public static async Task<GetTimeResult> GetTime(GetTimeRequest request) => 
            await PlayFabApiCall<GetTimeRequest, GetTimeResult>(request, PlayFabClientAPI.GetTime);

        public static async Task<GetTitleDataResult> GetTitleData(GetTitleDataRequest request) => 
            await PlayFabApiCall<GetTitleDataRequest, GetTitleDataResult>(request, PlayFabClientAPI.GetTitleData);
        
        private static async Task<TResult> PlayFabApiCall<TRequest, TResult>(TRequest request, PlayFabAction<TRequest, TResult> apiCall) 
            where TResult : PlayFabResultCommon
        {
            TResult result = null;
            PlayFabError error = null;

            apiCall.Invoke(request, res => result = res, err => error = err);
            await UniTask.WaitUntil(() => result != null || error != null);
            if (error != null)
                throw new Exception(error.ErrorMessage);

            return result;
        }
        
        private delegate void PlayFabAction<in TRequest, out TResult>(
            TRequest request,
            Action<TResult> onSuccess,
            Action<PlayFabError> onError,
            object customData = null,
            Dictionary<string, string> extraHeaders = null);
    }
}