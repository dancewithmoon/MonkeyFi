using System.Threading.Tasks;
using PlayFab.ClientModels;
using Utils;

namespace Services.Login
{
    public class PlayfabAuthorizationService : IAuthorizationService
    {
        private readonly UserDataService _userDataService;
        private UserAccountInfo _userAccountInfo;
        private PlayerProfileModel _playerProfile;

        public bool NewlyCreatedAccount { get; private set; }
        public string UserUniqueId => _userAccountInfo.Username;

        public PlayfabAuthorizationService(UserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        public async Task Authorize()
        {
            LoginResult loginResult = await LoginWithCustomId();
            _userAccountInfo = loginResult.InfoResultPayload.AccountInfo;
            _playerProfile = loginResult.InfoResultPayload.PlayerProfile;
            NewlyCreatedAccount = loginResult.NewlyCreated;

            if (UniqueIdNotSet())
                await SetupUniqueId();
            
            if (IsUsernameChanged())
                await UpdateUsername();
        }

        private async Task<LoginResult> LoginWithCustomId()
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = _userDataService.Id.ToString(),
                CreateAccount = true,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetPlayerProfile = true,
                    GetUserAccountInfo = true
                }
            };

            return await PlayFabClientAsyncAPI.LoginWithCustomID(request);
        }

        private async Task<AddUsernamePasswordResult> SetupUniqueId()
        {
            AddUsernamePasswordRequest request = new()
            {
                Username = _userDataService.Id.ToString(),
                Password = "password" + _userDataService.Id,
                Email = "email" + _userDataService.Id + "@monkey.fi"
            };

            return await PlayFabClientAsyncAPI.AddUsernamePassword(request);
        }

        private async Task<UpdateUserTitleDisplayNameResult> UpdateUsername()
        {
            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = _userDataService.Username
            };

            return await PlayFabClientAsyncAPI.UpdateUserTitleDisplayName(request);
        }

        private bool IsUsernameChanged() =>
            _playerProfile == null || _playerProfile.DisplayName != _userDataService.Username;

        private bool UniqueIdNotSet() =>
            string.IsNullOrEmpty(_userAccountInfo.Username);
    }
}