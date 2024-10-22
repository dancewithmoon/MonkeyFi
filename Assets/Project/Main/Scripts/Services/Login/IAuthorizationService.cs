using System.Threading.Tasks;

namespace Services.Login
{
    public interface IAuthorizationService
    {
        bool NewlyCreatedAccount { get; }
        Task Authorize();
    }
}