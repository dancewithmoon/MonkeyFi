using System;

namespace Services.Telegram
{
    public class MockedTelegramService : ITelegramService
    {
        public TelegramUserData TelegramUser { get; private set; }
        public event Action OnUserDataLoadedEvent;

        public void Initialize()
        {
            TelegramUser = new TelegramUserData(1337, "username");
            OnUserDataLoadedEvent?.Invoke();
        }
    }
}