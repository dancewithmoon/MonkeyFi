﻿using System.Collections.Generic;
using Infrastructure.Factory;
using Services.TonWallet;
using UI.Elements;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Windows
{
    public class ConnectWalletPopup : BaseWindow
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Transform _content;
        
        private TonWalletService _tonWalletService;
        private IGameFactory _gameFactory;
        
        private readonly Dictionary<WalletModel, WalletItem> _wallets = new();

        [Inject]
        private void Construct(TonWalletService tonWalletService, IGameFactory gameFactory)
        {
            _tonWalletService = tonWalletService;
            _gameFactory = gameFactory;
        }

        protected override void OnWindowShow() => 
            _closeButton.onClick.AddListener(Close);

        protected override void OnWindowHide() => 
            _closeButton.onClick.RemoveListener(Close);

        public override void DrawWindow()
        {
            foreach (WalletModel walletModel in _tonWalletService.Wallets.Values)
                DrawWalletItem(walletModel);
        }

        private async void DrawWalletItem(WalletModel walletModel)
        {
            if (_wallets.TryGetValue(walletModel, out WalletItem item) == false)
            {
                item = await _gameFactory.CreateWalletItem(walletModel, _content);
                _wallets[walletModel] = item;
            }

            item.Draw();
        }
    }
}