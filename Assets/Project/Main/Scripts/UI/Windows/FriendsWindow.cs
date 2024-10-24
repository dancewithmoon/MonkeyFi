using System.Collections.Generic;
using Infrastructure.Factory;
using Services;
using Services.Referral;
using TMPro;
using UI.Elements;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Windows
{
    public class FriendsWindow : BaseWindow
    {
        [SerializeField] private Button _inviteFriendsButton;
        
        [Header("No friends")]
        [SerializeField] private TMP_Text _noFriendsText;
        [SerializeField] private TMP_Text _usernameText;
        
        [Header("Has friends")]
        [SerializeField] private TMP_Text _yourFriendsText;
        [SerializeField] private GameObject _friendsScroll;
        [SerializeField] private Transform _content;

        private UserDataService _userDataService;
        private IReferralService _referralService;
        private IGameFactory _gameFactory;
        
        private readonly Dictionary<ReferralModel, FriendItem> _friends = new();

        [Inject]
        private void Construct(UserDataService userDataService, IReferralService referralService, IGameFactory gameFactory)
        {
            _userDataService = userDataService;
            _referralService = referralService;
            _gameFactory = gameFactory;
        }

        public override void DrawWindow()
        {
            if (_referralService.Referrals.Count == 0)
                DrawNoFriendsState();
            else
                DrawHasFriendsState();
        }

        protected override void OnWindowShow() => 
            _inviteFriendsButton.onClick.AddListener(OnInviteFriendsClick);

        protected override void OnWindowHide() => 
            _inviteFriendsButton.onClick.RemoveListener(OnInviteFriendsClick);

        private void DrawNoFriendsState()
        {
            _noFriendsText.gameObject.SetActive(true);
            _usernameText.gameObject.SetActive(true);
            _usernameText.text = _userDataService.Username;
            
            _yourFriendsText.gameObject.SetActive(false);
            _friendsScroll.SetActive(false);
        }

        private void DrawHasFriendsState()
        {
            _noFriendsText.gameObject.SetActive(false);
            _usernameText.gameObject.SetActive(false);
            
            _yourFriendsText.gameObject.SetActive(true);
            _friendsScroll.SetActive(true);
            _referralService.Referrals.ForEach(DrawFriendItem);
        }
        
        private async void DrawFriendItem(ReferralModel friendModel)
        {
            if (_friends.TryGetValue(friendModel, out FriendItem item) == false)
            {
                item = await _gameFactory.CreateFriendItem(friendModel, _content);
                _friends[friendModel] = item;
            }

            item.Draw();
        }
        
        private void OnInviteFriendsClick()
        {
            
        }
    }
}