namespace Plugins.Telegram
{
    [System.Serializable]
    public class TelegramUserDto
    {
        public long id;
        public string first_name;
        public string last_name;
        public string username;
        public string language_code;
        public bool allows_write_to_pm;
    }
}