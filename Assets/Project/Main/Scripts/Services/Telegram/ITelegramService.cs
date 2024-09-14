using System;

namespace Services.Telegram
{
    public interface ITelegramService
    {
        TelegramUserData TelegramUser { get; }
        event Action OnUserDataLoadedEvent;

        void Initialize();
    }
}