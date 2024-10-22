namespace Services.Telegram
{
    public interface ITelegramService
    {
        TelegramUserData TelegramUser { get; }
        string ReferralCode { get; }

        void Initialize();
    }
}