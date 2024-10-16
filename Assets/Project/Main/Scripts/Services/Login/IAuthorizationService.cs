using System;

namespace Services.Login
{
    public interface IAuthorizationService
    {
        bool NewlyCreatedAccount { get; }
        void Authorize();
        event Action OnAuthorizationSuccessEvent;
    }
}