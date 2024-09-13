using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Windows
{
    public class ClickerWindow : BaseWindow
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _counterText;
        [SerializeField] private int _maxCount;
        
        private UserProgressService _userProgressService;

        [Inject]
        private void Construct(UserProgressService userProgressService)
        {
            _userProgressService = userProgressService;
        }
        
        protected override void OnWindowShow()
        {
            _button.onClick.AddListener(OnClick);
            _userProgressService.OnCounterUpdateEvent += UpdateCounterView;
        }

        protected override void OnWindowHide()
        {
            _button.onClick.RemoveListener(OnClick);
            _userProgressService.OnCounterUpdateEvent -= UpdateCounterView;
        }

        public override void DrawWindow()
        {
            UpdateCounterView();
        }

        private void OnClick() => 
            _userProgressService.IncreaseCounter();

        private void UpdateCounterView()
        {
            if (_userProgressService.Counter >= _maxCount)
            {
                _counterText.text = "Stop wasting your time, bruh...";
                return;
            }
        
            _counterText.text = _userProgressService.Counter.ToString();
        }
    }
}