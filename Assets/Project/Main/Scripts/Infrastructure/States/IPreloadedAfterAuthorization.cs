using System.Threading.Tasks;

namespace Infrastructure.States
{
    public interface IPreloadedAfterAuthorization
    {
        Task Preload();
    }
}