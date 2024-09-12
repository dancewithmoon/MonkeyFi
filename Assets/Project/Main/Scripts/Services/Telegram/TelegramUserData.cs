namespace Services.Telegram
{
    public class TelegramUserData
    {
        public string Username { get; }
        
        public TelegramUserData(string username)
        {
            Username = username;
        }
    }
}