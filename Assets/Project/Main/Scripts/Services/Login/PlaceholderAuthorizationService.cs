using System.Threading.Tasks;

namespace Services.Login
{
    public class PlaceholderAuthorizationService : IAuthorizationService
    {
        public bool NewlyCreatedAccount => false;

        public Task Authorize() => Task.CompletedTask;
    }
}