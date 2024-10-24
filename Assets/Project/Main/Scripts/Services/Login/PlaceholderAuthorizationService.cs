using System.Threading.Tasks;

namespace Services.Login
{
    public class PlaceholderAuthorizationService : IAuthorizationService
    {
        public bool NewlyCreatedAccount => false;
        public string UserUniqueId => "1337";

        public Task Authorize() => Task.CompletedTask;
    }
}