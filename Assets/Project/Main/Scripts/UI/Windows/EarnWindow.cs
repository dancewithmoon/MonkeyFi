using Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Windows
{
    public class EarnWindow : BaseWindow
    {
        [SerializeField] private Button _connectWalletButton;
        private IWindowService _windowService;

        [Inject]
        private void Construct(IWindowService windowService) => 
            _windowService = windowService;

        protected override void OnWindowShow() => 
            _connectWalletButton.onClick.AddListener(ConnectWallet);

        protected override void OnWindowHide() => 
            _connectWalletButton.onClick.RemoveListener(ConnectWallet);

        private void ConnectWallet() =>
            _windowService.ShowModalWindow(WindowType.ConnectWallet);
    }
}