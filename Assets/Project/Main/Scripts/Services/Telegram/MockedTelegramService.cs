using UnityEngine;

namespace Services.Telegram
{
    public class MockedTelegramService : ITelegramService, IShareService
    {
        public TelegramUserData TelegramUser { get; private set; }
        public string ReferralCode { get; private set; }

        public void Initialize()
        {
            TelegramUser = new TelegramUserData(1337, "username");
        }

        public void Share(string message, string url) => 
            Debug.Log($"You shared:\n{message}\n{url}");
    }
}