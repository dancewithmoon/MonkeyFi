using Services.Telegram;

namespace Services
{
    public class UserDataService
    {
        private readonly ITelegramService _telegramService;
        public string Username => _telegramService.TelegramUser.Username;
        
        public UserDataService(ITelegramService telegramService)
        {
            _telegramService = telegramService;
        }
    }
}