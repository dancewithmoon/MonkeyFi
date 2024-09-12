using Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.Windows
{
    public class FriendsWindow : BaseWindow
    {
        [SerializeField] private TMP_Text _userDataText;
        
        private UserDataService _userDataService;

        [Inject]
        private void Construct(UserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        public override void DrawWindow()
        {
            _userDataText.text = _userDataService.Username;
        }
    }
}