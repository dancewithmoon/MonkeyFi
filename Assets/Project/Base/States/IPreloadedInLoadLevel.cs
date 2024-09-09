using System.Threading.Tasks;

namespace Base.States
{
    public interface IPreloadedInLoadLevel
    {
        Task Preload();
    }

    public interface ICleanUp
    {
        void CleanUp();
    }
}