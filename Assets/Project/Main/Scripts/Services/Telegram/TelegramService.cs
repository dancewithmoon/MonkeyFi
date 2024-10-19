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
            TelegramUserDto telegramUserData = TelegramBridge.GetTelegramUserData();
            TelegramUser = new TelegramUserData(telegramUserData.id, telegramUserData.username);
        }
    }
}