using Services;
using Services.TonWallet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Windows
{
    public class EarnWindow : BaseWindow
    {
        [SerializeField] private Button _connectWalletButton;
        [SerializeField] private TMP_Text _connectWalletText;
        
        private IWindowService _windowService;
        private TonWalletService _tonWalletService;

        [Inject]
        private void Construct(IWindowService windowService, TonWalletService tonWalletService)
        {
            _windowService = windowService;
            _tonWalletService = tonWalletService;
        }

        protected override void OnWindowShow()
        {
            _connectWalletButton.onClick.AddListener(ConnectWallet);
            _tonWalletService.OnWalletConnectedEvent += DrawWindow;
        }

        protected override void OnWindowHide()
        {
            _connectWalletButton.onClick.RemoveListener(ConnectWallet);
            _tonWalletService.OnWalletConnectedEvent -= DrawWindow;
        }

        public override void DrawWindow() => 
            DrawConnectWalletButton();

        private void DrawConnectWalletButton()
        {
            _connectWalletButton.interactable = !_tonWalletService.IsConnected;
            _connectWalletText.text = _tonWalletService.IsConnected ? _tonWalletService.WalletAddress : "Connect Wallet";
        }

        private void ConnectWallet() => 
            _windowService.ShowModalWindow(WindowType.ConnectWallet);
    }
}