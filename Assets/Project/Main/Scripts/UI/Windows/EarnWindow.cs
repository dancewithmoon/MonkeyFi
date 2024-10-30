using Services.TonWallet;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Windows
{
    public class EarnWindow : BaseWindow
    {
        [SerializeField] private Button _connectWalletButton;
        private TonWalletService _tonWalletService;

        [Inject]
        private void Construct(TonWalletService tonWalletService) => 
            _tonWalletService = tonWalletService;

        protected override void OnWindowShow() => 
            _connectWalletButton.onClick.AddListener(ConnectWallet);

        protected override void OnWindowHide() => 
            _connectWalletButton.onClick.RemoveListener(ConnectWallet);

        private void ConnectWallet() => 
            _tonWalletService.ConnectWallet();
    }
}