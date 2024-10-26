using Plugins.Telegram;

namespace Services.Telegram
{
    public class TelegramService : ITelegramService, IShareService
    {
        public string ReferralCode { get; private set; }
        public TelegramUserData TelegramUser { get; private set; }
        
        public void Initialize()
        {
            ReferralCode = TelegramBridge.GetTelegramStartParam();
            TelegramUser = CreateUserData(TelegramBridge.GetTelegramUserData());
        }

        public void Share(string message, string url) => 
            TelegramBridge.Share(message, url);

        private static TelegramUserData CreateUserData(TelegramUserDto telegramUser) =>
            new(telegramUser.id, telegramUser.username);
    }
}