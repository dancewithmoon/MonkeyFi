namespace Services.Telegram
{
    public interface ITelegramService
    {
        TelegramUserData TelegramUser { get; }

        void Initialize();
    }
}