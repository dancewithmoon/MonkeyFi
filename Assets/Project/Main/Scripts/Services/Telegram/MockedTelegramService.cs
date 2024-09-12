namespace Services.Telegram
{
    public class MockedTelegramService : ITelegramService
    {
        public TelegramUserData TelegramUser { get; private set; }
        
        public void Initialize()
        {
            TelegramUser = new TelegramUserData("username");
        }
    }
}