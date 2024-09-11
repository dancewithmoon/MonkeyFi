using System;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    [RequireComponent(typeof(Button))]
    public class BottomPanelButton : MonoBehaviour
    {
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _selectedColor;
        
        [SerializeField] private WindowType _destinationWindow;
        public WindowType DestinationWindow => _destinationWindow;

        private Button _button;

        public event Action<BottomPanelButton> OnClickEvent;

        private void Awake() => 
            _button = GetComponent<Button>();

        private void OnEnable() => 
            _button.onClick.AddListener(OnClick);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnClick);

        private void OnClick() => 
            OnClickEvent?.Invoke(this);

        public void SetSelected(bool isSelected) => 
            _button.image.color = isSelected ? _selectedColor : _defaultColor;
    }
}