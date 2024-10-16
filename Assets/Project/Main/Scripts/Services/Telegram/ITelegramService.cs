using System;

namespace Services.Telegram
{
    public interface ITelegramService
    {
        TelegramUserData TelegramUser { get; }
        string ReferralCode { get; }
        event Action OnUserDataLoadedEvent;

        void Initialize();
    }
}