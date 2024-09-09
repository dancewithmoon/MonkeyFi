using System.Threading.Tasks;

namespace Base.States
{
    public interface IPreloadedInBootstrap
    {
        Task Preload();
    }
}