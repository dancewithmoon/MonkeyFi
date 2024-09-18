using Models;
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
        [SerializeField] private TMP_Text _energyText;

        private ClickerModel _clickerModel;

        [Inject]
        private void Construct(ClickerModel clickerModel)
        {
            _clickerModel = clickerModel;
        }
        
        protected override void OnWindowShow()
        {
            _button.onClick.AddListener(OnClick);
            _clickerModel.OnStateChangedEvent += DrawWindow;
        }

        protected override void OnWindowHide()
        {
            _button.onClick.RemoveListener(OnClick);
            _clickerModel.OnStateChangedEvent -= DrawWindow;
        }

        public override void DrawWindow()
        {
            DrawEnergy();
            DrawCounter();
        }

        private void DrawEnergy() => 
            _energyText.text = $"{_clickerModel.CurrentEnergy}/{_clickerModel.MaxEnergy}";

        private void DrawCounter()
        {
            if (_clickerModel.CurrentEnergy == 0)
            {
                _counterText.text = "Stop wasting your time, bruh...";
                return;
            }
        
            _counterText.text = _clickerModel.Points.ToString();
        }

        private void OnClick() => 
            _clickerModel.Click();
    }
}