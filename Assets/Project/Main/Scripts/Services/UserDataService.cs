using Services.Telegram;

namespace Services
{
    public class UserDataService
    {
        private readonly TelegramService _telegramService;
        public string Username => _telegramService.TelegramUser.Username;
        
        public UserDataService(TelegramService telegramService)
        {
            _telegramService = telegramService;
        }
    }
}