using Plugins.Telegram;

namespace Services.Telegram
{
    public class TelegramService : ITelegramService
    {
        public string ReferralCode { get; private set; }
        public TelegramUserData TelegramUser { get; private set; }
        
        public void Initialize()
        {
            ReferralCode = TelegramBridge.GetTelegramStartParam();
            TelegramUser = CreateUserData(TelegramBridge.GetTelegramUserData());
        }

        private static TelegramUserData CreateUserData(TelegramUserDto telegramUser) =>
            new(telegramUser.id, telegramUser.username);
    }
}