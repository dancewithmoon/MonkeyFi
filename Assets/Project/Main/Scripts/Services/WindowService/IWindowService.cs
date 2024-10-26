using UI;
using UI.Windows;

namespace Services
{
    public interface IWindowService
    {
        HudOverlay Hud { get; }
        void ShowHudOverlay();
        void ShowWindow(WindowType windowType);
        void ShowWindow<TPayload>(WindowType windowType, TPayload payload);
        void HideWindow(WindowType windowType);
        void ClearWindows();
        void ClearHistory();
    }
}