using System.Collections.Generic;
using Infrastructure.Factory;
using Services;
using Services.Quests;
using Services.TonWallet;
using TMPro;
using UI.Elements;
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
        [SerializeField] private Button _sendTransactionButton;

        [Header("Quests")]
        [SerializeField] private Transform _questsContainer;
        
        private IWindowService _windowService;
        private TonWalletService _tonWalletService;
        private IGameFactory _gameFactory;
        private IQuestsService _questsService;

        private readonly Dictionary<QuestModel, QuestItem> _quests = new();

        [Inject]
        private void Construct(IWindowService windowService, TonWalletService tonWalletService, IGameFactory gameFactory, IQuestsService questsService)
        {
            _windowService = windowService;
            _tonWalletService = tonWalletService;
            _gameFactory = gameFactory;
            _questsService = questsService;
        }

        protected override void OnWindowShow()
        {
            _connectWalletButton.onClick.AddListener(ConnectWallet);
            _disconnectWalletButton.onClick.AddListener(DisconnectWallet);
            _sendTransactionButton.onClick.AddListener(SendTransaction);
            _tonWalletService.OnWalletUpdateEvent += DrawWindow;
        }
        
        protected override void OnWindowHide()
        {
            _connectWalletButton.onClick.RemoveListener(ConnectWallet);
            _disconnectWalletButton.onClick.RemoveListener(DisconnectWallet);
            _sendTransactionButton.onClick.RemoveListener(SendTransaction);
            _tonWalletService.OnWalletUpdateEvent -= DrawWindow;
        }

        public override void DrawWindow()
        {
            DrawConnectWalletButton();
            DrawSendTransactionButton();
            DrawQuests();
        }

        private void DrawConnectWalletButton()
        {
            _connectWalletButton.interactable = !_tonWalletService.IsConnected;
            _connectWalletText.text = _tonWalletService.IsConnected ? _tonWalletService.WalletAddress : "Connect Wallet";
            _disconnectWalletButton.gameObject.SetActive(_tonWalletService.IsConnected);
        }

        private void DrawSendTransactionButton() => 
            _sendTransactionButton.interactable = _tonWalletService.IsConnected;

        private void DrawQuests() => 
            _questsService.Quests.ForEach(DrawQuestItem);

        private void ConnectWallet() => 
            _windowService.ShowModalWindow(WindowType.ConnectWallet);

        private void DisconnectWallet() => 
            _tonWalletService.DisconnectWallet();
        
        private void SendTransaction() => 
            _tonWalletService.SendTransaction();
        
        private async void DrawQuestItem(QuestModel questModel)
        {
            if (_quests.TryGetValue(questModel, out QuestItem item) == false)
            {
                item = await _gameFactory.CreateQuestItem(questModel, _questsContainer);
                _quests[questModel] = item;
            }

            item.Draw();
        }
    }
}