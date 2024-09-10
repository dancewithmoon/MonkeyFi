using System.Collections.Generic;
using Infrastructure.Factory;
using UI.Windows;

namespace Services
{
    public class WindowService : IWindowService
    {
        private readonly IGameFactory _gameFactory;
        private readonly Dictionary<WindowType, BaseWindow> _windows = new();

        public WindowService(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void ShowWindow(WindowType windowType)
        {
            if (_windows.ContainsKey(windowType))
                _windows[windowType].Visible = true;
            else
                _windows[windowType] = _gameFactory.CreateWindow(windowType);
        }

        public void ShowWindow<TPayload>(WindowType windowType, TPayload payload)
        {
            ShowWindow(windowType);
            if (_windows[windowType] is PayloadedWindow<TPayload> payloadedWindow)
                payloadedWindow.SetPayload(payload);
        }

        public void HideWindow(WindowType windowType)
        {
            _windows[windowType].Visible = false;
        }
    }
}