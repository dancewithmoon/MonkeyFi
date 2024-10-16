using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace Services.Login
{
    public class PlayfabAuthorizationService : IAuthorizationService
    {
        private readonly UserDataService _userDataService;

        public bool NewlyCreatedAccount { get; private set; }

        public event Action OnAuthorizationSuccessEvent;

        public PlayfabAuthorizationService(UserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        public void Authorize()
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = _userDataService.Id.ToString(),
                CreateAccount = true,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetPlayerProfile = true
                }
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnPlayfabError);
        }

        private void OnLoginSuccess(LoginResult result)
        {
            NewlyCreatedAccount = result.NewlyCreated;
            
            if (IsUsernameChanged(result))
                UpdateUsername();
            else
                OnAuthorizationSuccessEvent?.Invoke();
        }
        
        private bool IsUsernameChanged(LoginResult result) => 
            result.InfoResultPayload.PlayerProfile.DisplayName != _userDataService.Username;
        
        private void UpdateUsername()
        {
            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = _userDataService.Username
            };
            PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUsernameUpdate, OnPlayfabError);
        }

        private void OnUsernameUpdate(UpdateUserTitleDisplayNameResult result) => 
            OnAuthorizationSuccessEvent?.Invoke();

        private void OnPlayfabError(PlayFabError error) => 
            Debug.LogError("Playfab login error: " + error);
    }
}