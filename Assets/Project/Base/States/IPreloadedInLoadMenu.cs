using System.Threading.Tasks;

namespace Base.States
{
    public interface IPreloadedInLoadMenu
    {
        Task Preload();
    }

    public interface ICleanUp
    {
        void CleanUp();
    }
}