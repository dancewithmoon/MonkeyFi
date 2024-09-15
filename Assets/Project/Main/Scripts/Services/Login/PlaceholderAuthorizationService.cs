using System;

namespace Services.Login
{
    public class PlaceholderAuthorizationService : IAuthorizationService
    {
        public event Action OnAuthorizationSuccessEvent;
        
        public void Authorize() => OnAuthorizationSuccessEvent?.Invoke();
    }
}