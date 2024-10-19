namespace Services.Telegram
{
    public class MockedTelegramService : ITelegramService
    {
        public TelegramUserData TelegramUser { get; private set; }
        public string ReferralCode { get; private set; }

        public void Initialize()
        {
            TelegramUser = new TelegramUserData(1337, "username");
        }
    }
}