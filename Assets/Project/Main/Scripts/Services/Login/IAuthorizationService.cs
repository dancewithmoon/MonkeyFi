using System.Threading.Tasks;

namespace Services.Login
{
    public interface IAuthorizationService
    {
        bool NewlyCreatedAccount { get; }
        string UserUniqueId { get; }
        Task Authorize();
    }
}