using System.Collections.Generic;
using Services;
using UnityEngine;
using Zenject;

namespace UI.Elements
{
    public class BottomPanel : MonoBehaviour
    {
        [SerializeField] private List<BottomPanelButton> _buttons;

        private IWindowService _windowService;
        private BottomPanelButton _selectedButton;
        
        [Inject]
        private void Construct(IWindowService windowService)
        {
            _windowService = windowService;
        }
        
        private void OnEnable() => 
            _buttons.ForEach(button => button.OnClickEvent += OnButtonClick);

        private void OnDisable() => 
            _buttons.ForEach(button => button.OnClickEvent -= OnButtonClick);

        private void Start()
        {
            _selectedButton = _buttons[0];
            UpdateButtonsSelection();
        }

        private void OnButtonClick(BottomPanelButton selectedButton)
        {
            _selectedButton = selectedButton;
            _windowService.ShowWindow(_selectedButton.DestinationWindow);
            UpdateButtonsSelection();
        }

        private void UpdateButtonsSelection() => 
            _buttons.ForEach(UpdateButtonSelection);

        private void UpdateButtonSelection(BottomPanelButton button) => 
            button.SetSelected(button == _selectedButton);
    }
}