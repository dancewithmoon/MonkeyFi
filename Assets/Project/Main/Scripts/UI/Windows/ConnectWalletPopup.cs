using Services.TonWallet;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Windows
{
    public class ConnectWalletPopup : BaseWindow
    {
        [SerializeField] private Button _closeButton;
        private TonWalletService _tonWalletService;

        [Inject]
        private void Construct(TonWalletService tonWalletService)
        {
            _tonWalletService = tonWalletService;
        }

        protected override void OnWindowShow() => 
            _closeButton.onClick.AddListener(Close);

        protected override void OnWindowHide() => 
            _closeButton.onClick.RemoveListener(Close);
    }
}