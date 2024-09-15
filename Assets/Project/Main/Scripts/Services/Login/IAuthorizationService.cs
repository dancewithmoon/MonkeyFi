using System;

namespace Services.Login
{
    public interface IAuthorizationService
    {
        void Authorize();
        event Action OnAuthorizationSuccessEvent;
    }
}