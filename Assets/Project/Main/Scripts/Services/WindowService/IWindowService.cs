using UI;
using UI.Windows;

namespace Services
{
    public interface IWindowService
    {
        HudOverlay Hud { get; }
        void ShowHudOverlay();
        void ShowWindow(WindowType windowType, bool callDraw = true);
        void ShowWindow<TPayload>(WindowType windowType, TPayload payload);
        void HideWindow(WindowType windowType);
        void ClearWindows();
    }
}