using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Services;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace UI.Windows
{
    public class GamesWindow : BaseWindow
    {
        [SerializeField] private SerializedDictionary<Button, WindowType> _buttons;
        
        private readonly Dictionary<Button, UnityAction> _buttonActions = new();
        private IWindowService _windowService;

        [Inject]
        private void Construct(IWindowService windowService)
        {
            _windowService = windowService;
        }

        public override void OnWindowCreated()
        {
            foreach ((Button button, WindowType windowType) in _buttons)
                _buttonActions[button] = () => ShowWindow(windowType);
        }

        protected override void OnWindowShow()
        {
            foreach ((Button button, UnityAction action) in _buttonActions)
                button.onClick.AddListener(action);
        }

        protected override void OnWindowHide()
        {
            foreach ((Button button, UnityAction action) in _buttonActions)
                button.onClick.RemoveListener(action);
        }

        private void ShowWindow(WindowType windowType) => 
            _windowService.ShowWindow(windowType);
    }
}