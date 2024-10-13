using System;
using UI.Elements;
using UI.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HudOverlay : BaseWindow
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private BottomPanel _bottomPanel;
        
        public event Action OnBackButtonClickEvent;

        public void SetBackButtonActive(bool isActive) => 
            _backButton.gameObject.SetActive(isActive);

        public void SetBottomPanelActive(bool isActive) => 
            _bottomPanel.gameObject.SetActive(isActive);

        protected override void OnWindowShow() =>
            _backButton.onClick.AddListener(InvokeBackButtonClickEvent);

        protected override void OnWindowHide() =>
            _backButton.onClick.RemoveListener(InvokeBackButtonClickEvent);

        private void InvokeBackButtonClickEvent() => OnBackButtonClickEvent?.Invoke();
    }
}