using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace Services.Login
{
    public class PlayfabAuthorizationService : IAuthorizationService
    {
        private readonly UserDataService _userDataService;

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
                CreateAccount = true
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnPlayfabError);
        }

        private void OnLoginSuccess(LoginResult result) => 
            OnAuthorizationSuccessEvent?.Invoke();

        private void OnPlayfabError(PlayFabError error) => 
            Debug.LogError("Playfab login error: " + error);
    }
}