using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class ClickerWindow : BaseWindow
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _counterText;
        [SerializeField] private int _maxCount;
        private int _counter = 0;
    
        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        public override void DrawWindow()
        {
            UpdateCounterView();
        }

        private void OnClick()
        {
            _counter++;
            UpdateCounterView();
        }

        private void UpdateCounterView()
        {
            if (_counter >= _maxCount)
            {
                _counterText.text = "Stop wasting your time, bruh...";
                return;
            }
        
            _counterText.text = _counter.ToString();
        }
    }
}