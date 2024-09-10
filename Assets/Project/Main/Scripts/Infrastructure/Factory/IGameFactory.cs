using Services;
using UI.Windows;

namespace Infrastructure.Factory
{
    public interface IGameFactory
    {
        void CreateUIRoot();
        public BaseWindow CreateWindow(WindowType windowType);
    }
}