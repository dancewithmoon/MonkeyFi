namespace Services.Telegram
{
    public class TelegramUserData
    {
        public long Id { get; }
        public string Username { get; }

        public TelegramUserData(long id, string username)
        {
            Id = id;
            Username = username;
        }
    }
}