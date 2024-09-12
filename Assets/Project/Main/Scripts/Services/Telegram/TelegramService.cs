using Plugins.Telegram;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Services.Telegram
{
    public class TelegramService
    {
        public TelegramUserData TelegramUser { get; private set; }
        
        public void Initialize()
        {
            InitializeBridge();
            TelegramBridge.RequestUserData();
        }

        private void InitializeBridge()
        {
            var bridge = new GameObject("TelegramBridge").AddComponent<TelegramBridge>();
            Object.DontDestroyOnLoad(bridge);
            bridge.OnUserDataReceiveEvent += OnUserDataReceive;
        }
        
        private void OnUserDataReceive(TelegramUserDto user)
        {
            TelegramUser = new TelegramUserData(user.username);
        }
    }
}