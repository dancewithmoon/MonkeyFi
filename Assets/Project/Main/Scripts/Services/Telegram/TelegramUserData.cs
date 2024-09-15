namespace Services.Telegram
{
    public class TelegramUserData
    {
        public int Id { get; }
        public string Username { get; }

        public TelegramUserData(int id, string username)
        {
            Id = id;
            Username = username;
        }
    }
}