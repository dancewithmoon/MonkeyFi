using System.Collections.Generic;
using Infrastructure.Factory;
using UI.Windows;

namespace Services
{
    public class WindowService : IWindowService
    {
        private readonly IGameFactory _gameFactory;
        private readonly Dictionary<WindowType, BaseWindow> _windows = new();

        private WindowType _currentWindow = WindowType.None;
        
        public WindowService(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void ShowWindow(WindowType windowType)
        {
            HideWindow(_currentWindow);

            if(windowType == WindowType.None)
                return;
            
            if (_windows.ContainsKey(windowType))
                _windows[windowType].Visible = true;
            else
                _windows[windowType] = _gameFactory.CreateWindow(windowType);
            
            _currentWindow = windowType;
        }

        public void ShowWindow<TPayload>(WindowType windowType, TPayload payload)
        {
            HideWindow(_currentWindow);
            
            if(windowType == WindowType.None)
                return;
            
            ShowWindow(windowType);
            if (_windows[windowType] is PayloadedWindow<TPayload> payloadedWindow)
                payloadedWindow.SetPayload(payload);
        }

        public void HideWindow(WindowType windowType)
        {
            if(windowType == WindowType.None)
                return;
            
            _windows[windowType].Visible = false;
        }
    }
}