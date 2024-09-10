﻿namespace Services
{
    public interface IWindowService
    {
        void ShowWindow(WindowType windowType);
        void ShowWindow<TPayload>(WindowType windowType, TPayload payload);
        void HideWindow(WindowType windowType);
    }
}