namespace Plugins.Telegram
{
    [System.Serializable]
    public class TelegramUserDto
    {
        public int id;
        public bool is_bot;
        public string first_name;
        public string last_name;
        public string username;
        public string language_code;
        public bool is_premium;
        public bool added_to_attachment_menu;
        public bool allows_write_to_pm;
        public string photo_url;
    }
}