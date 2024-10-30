using System.Collections.Generic;
using Infrastructure.Factory;
using UI;
using UI.Windows;
using Object = UnityEngine.Object;

namespace Services
{
    public class WindowService : IWindowService
    {
        private readonly IGameFactory _gameFactory;
        private readonly Dictionary<WindowType, BaseWindow> _windows = new();
        private readonly List<WindowType> _history = new();

        private WindowType _currentWindow = WindowType.None;

        public HudOverlay Hud { get; private set; }
        private int HistoryLength => _history.Count;
        private WindowType LastShownWindow => _history.Count > 0 ? _history[^1] : WindowType.None;

        public WindowService(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public async void ShowHudOverlay()
        {
            if (Hud == null)
            {
                Hud = await _gameFactory.CreateHudOverlay();
                Hud.OnBackButtonClickEvent += GoBack;
            }

            Hud.Visible = true;
        }

        public void ShowWindow(WindowType windowType)
        {
            HideWindow(_currentWindow);

            if (TryShowWindow(windowType))
                _windows[windowType].DrawWindow();
        }

        public void ShowWindow<TPayload>(WindowType windowType, TPayload payload)
        {
            HideWindow(_currentWindow);

            if (TryShowWindow(windowType))
            {
                if (_windows[windowType] is PayloadedWindow<TPayload> payloadedWindow)
                    payloadedWindow.SetPayload(payload);

                _windows[windowType].DrawWindow();
            }
        }

        public void ShowModalWindow(WindowType windowType)
        {
            if (TryShowModalWindow(windowType))
                _windows[windowType].DrawWindow();
        }

        public void ShowModalWindow<TPayload>(WindowType windowType, TPayload payload)
        {
            if (TryShowModalWindow(windowType))
            {
                if (_windows[windowType] is PayloadedWindow<TPayload> payloadedWindow)
                    payloadedWindow.SetPayload(payload);

                _windows[windowType].DrawWindow();
            }
        }
        
        public void HideWindow(WindowType windowType)
        {
            if(windowType == WindowType.None)
                return;
            
            _windows[windowType].Visible = false;
        }

        public void ClearWindows()
        {
            foreach ((WindowType windowType, BaseWindow window) in _windows)
                Object.Destroy(window.gameObject);

            _windows.Clear();
            ClearHistory();
            _currentWindow = WindowType.None;
        }

        public void ClearHistory() => _history.Clear();

        private bool TryShowWindow(WindowType windowType)
        {
            if (windowType == WindowType.None)
                return false;

            if (_windows.ContainsKey(windowType))
                _windows[windowType].Visible = true;
            else
                _windows[windowType] = _gameFactory.CreateWindow(windowType);

            _currentWindow = windowType;
            
            AddCurrentWindowToHistory();
            Hud.SetBackButtonActive(HistoryLength > 1);

            return true;
        }
        
        private bool TryShowModalWindow(WindowType windowType)
        {
            if (windowType == WindowType.None)
                return false;

            if (_windows.ContainsKey(windowType))
                _windows[windowType].Visible = true;
            else
                _windows[windowType] = _gameFactory.CreateWindow(windowType);
            
            _windows[windowType].transform.SetAsLastSibling();
            
            return true;
        }

        private void GoBack()
        {
            if(_history.Count == 0)
                return;
            
            HideWindow(_currentWindow);
            _history.Remove(_currentWindow);
            ShowWindow(LastShownWindow);
        }

        private void AddCurrentWindowToHistory()
        {
            if(_currentWindow == LastShownWindow)
                return;
            
            if (_history.Contains(_currentWindow))
                _history.Remove(_currentWindow);

            _history.Add(_currentWindow);
        }
    }
}