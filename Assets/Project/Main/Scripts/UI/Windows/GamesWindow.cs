using Base.States;
using Infrastructure.States;
using Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Windows
{
    public class GamesWindow : BaseWindow
    {
        [SerializeField] private Button _clickerButton;
        [SerializeField] private Button _gameButton;
        
        private IWindowService _windowService;
        private IGameStateMachine _stateMachine;

        [Inject]
        private void Construct(IWindowService windowService, IGameStateMachine stateMachine)
        {
            _windowService = windowService;
            _stateMachine = stateMachine;
        }

        protected override void OnWindowShow()
        {
            _clickerButton.onClick.AddListener(ShowClicker);
            _gameButton.onClick.AddListener(ShowGame);
        }

        protected override void OnWindowHide()
        {
            _clickerButton.onClick.RemoveListener(ShowClicker);
            _gameButton.onClick.RemoveListener(ShowGame);
        }
        
        private void ShowClicker() => 
            _windowService.ShowWindow(WindowType.Clicker);

        private void ShowGame() =>
            _stateMachine.Enter<LoadMonkeyBusinessState>();
    }
}