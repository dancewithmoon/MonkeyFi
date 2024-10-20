using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;

namespace Utils
{
    public static class PlayFabClientAsyncAPI
    {
        public static async Task<LoginResult> LoginWithCustomID(LoginWithCustomIDRequest request)
        {
            LoginResult loginResult = null;
            PlayFabError error = null;

            PlayFabClientAPI.LoginWithCustomID(request, 
                result => loginResult = result, 
                err => error = err);
            
            await UniTask.WaitUntil(() => loginResult != null || error != null);
            if (error != null)
                throw new Exception(error.ErrorMessage);

            return loginResult;
        }

        public static async Task<GetAccountInfoResult> GetAccountInfo(GetAccountInfoRequest request)
        {
            GetAccountInfoResult getAccountInfoResult = null;
            PlayFabError error = null;

            PlayFabClientAPI.GetAccountInfo(request,
                result => getAccountInfoResult = result,
                err => error = err);
            
            await UniTask.WaitUntil(() => getAccountInfoResult != null || error != null);
            if (error != null)
                throw new Exception(error.ErrorMessage);

            return getAccountInfoResult;
        }

        public static async Task<GetUserDataResult> GetUserData(GetUserDataRequest request)
        {
            GetUserDataResult getUserDataResult = null;
            PlayFabError error = null;
            
            PlayFabClientAPI.GetUserData(request, 
                result => getUserDataResult = result, 
                err => error = err);
            
            await UniTask.WaitUntil(() => getUserDataResult != null || error != null);
            if (error != null)
                throw new Exception(error.ErrorMessage);

            return getUserDataResult;
        }
        
        public static async Task<UpdateUserDataResult> UpdateUserData(UpdateUserDataRequest request)
        {
            UpdateUserDataResult updateUserDataResult = null;
            PlayFabError error = null;
            
            PlayFabClientAPI.UpdateUserData(request, 
                result => updateUserDataResult = result, 
                err => error = err);
            
            await UniTask.WaitUntil(() => updateUserDataResult != null || error != null);
            if (error != null)
                throw new Exception(error.ErrorMessage);

            return updateUserDataResult;
        }

        public static async Task<UpdateUserTitleDisplayNameResult> UpdateUserTitleDisplayName(
            UpdateUserTitleDisplayNameRequest request)
        {
            UpdateUserTitleDisplayNameResult updateUserTitleDisplayNameResult = null;
            PlayFabError error = null;

            PlayFabClientAPI.UpdateUserTitleDisplayName(request,
                result => updateUserTitleDisplayNameResult = result,
                err => error = err);
            
            await UniTask.WaitUntil(() => updateUserTitleDisplayNameResult != null || error != null);
            if (error != null)
                throw new Exception(error.ErrorMessage);

            return updateUserTitleDisplayNameResult;
        }

        public static async Task<AddUsernamePasswordResult> AddUsernamePassword(AddUsernamePasswordRequest request)
        {
            AddUsernamePasswordResult addUsernamePasswordResult = null;
            PlayFabError error = null;

            PlayFabClientAPI.AddUsernamePassword(request,
                result => addUsernamePasswordResult = result,
                err => error = err);
            
            await UniTask.WaitUntil(() => addUsernamePasswordResult != null || error != null);
            if (error != null)
                throw new Exception(error.ErrorMessage);

            return addUsernamePasswordResult;
        }

        public static async Task<ExecuteCloudScriptResult> ExecuteCloudScript(ExecuteCloudScriptRequest request)
        {
            ExecuteCloudScriptResult executeCloudScriptResult = null;
            PlayFabError error = null;

            PlayFabClientAPI.ExecuteCloudScript(request, 
                result => executeCloudScriptResult = result,
                err => error = err);
            
            await UniTask.WaitUntil(() => executeCloudScriptResult != null || error != null);

            if (error != null)
                throw new Exception(error.ErrorMessage);

            return executeCloudScriptResult;
        }
    }
}