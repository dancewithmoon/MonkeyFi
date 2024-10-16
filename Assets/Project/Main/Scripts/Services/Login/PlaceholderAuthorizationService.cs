using System;

namespace Services.Login
{
    public class PlaceholderAuthorizationService : IAuthorizationService
    {
        public bool NewlyCreatedAccount => false;
        public event Action OnAuthorizationSuccessEvent;

        public void Authorize() => OnAuthorizationSuccessEvent?.Invoke();
    }
}