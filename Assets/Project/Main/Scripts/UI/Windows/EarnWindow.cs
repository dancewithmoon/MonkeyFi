﻿using Services;
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
        [SerializeField] private Button _disconnectWalletButton;

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
            _disconnectWalletButton.onClick.AddListener(DisconnectWallet);
            _tonWalletService.OnWalletUpdateEvent += DrawWindow;
        }

        protected override void OnWindowHide()
        {
            _connectWalletButton.onClick.RemoveListener(ConnectWallet);
            _disconnectWalletButton.onClick.RemoveListener(DisconnectWallet);
            _tonWalletService.OnWalletUpdateEvent -= DrawWindow;
        }

        public override void DrawWindow() => 
            DrawConnectWalletButton();

        private void DrawConnectWalletButton()
        {
            _connectWalletButton.interactable = !_tonWalletService.IsConnected;
            _connectWalletText.text = _tonWalletService.IsConnected ? _tonWalletService.WalletAddress : "Connect Wallet";
            _disconnectWalletButton.gameObject.SetActive(_tonWalletService.IsConnected);
        }

        private void ConnectWallet() => 
            _windowService.ShowModalWindow(WindowType.ConnectWallet);

        private void DisconnectWallet() => 
            _tonWalletService.DisconnectWallet();
    }
}