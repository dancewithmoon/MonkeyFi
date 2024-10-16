using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Plugins.Telegram
{
    public class TelegramBridge : MonoBehaviour
    {
        public event Action<TelegramUserDto> OnUserDataReceiveEvent;
        public event Action<string> OnStartParamReceived;
        
        public void ReceiveUserData(string data)
        {
            var user = JsonUtility.FromJson<TelegramUserDto>(data);
            OnUserDataReceiveEvent?.Invoke(user);
        }
        
        public void ReceiveStartParam(string startParam)
        {
            OnStartParamReceived?.Invoke(startParam);
        }
        
        [DllImport("__Internal")]
        public static extern void RequestUserData();
        
        [DllImport("__Internal")]
        public static extern void RequestStartParam();

        [DllImport("__Internal")]
        public static extern void ShowMainButton(string text);

        [DllImport("__Internal")]
        public static extern void HideMainButton();

        [DllImport("__Internal")]
        public static extern void MainButtonShowProgress();

        [DllImport("__Internal")]
        public static extern void MainButtonHideProgress();

        [DllImport("__Internal")]
        public static extern void ShowBackButton();

        [DllImport("__Internal")]
        public static extern void HideBackButton();

        [DllImport("__Internal")]
        public static extern void ShowAlert(string text);

        [DllImport("__Internal")]
        public static extern void ShowShareJoinCode(string code);

        [DllImport("__Internal")]
        public static extern void Ready();

        [DllImport("__Internal")]
        public static extern void Close();

        [DllImport("__Internal")]
        public static extern void Expand();

        [DllImport("__Internal")]
        public static extern void HapticFeedback(string level);

        [DllImport("__Internal")]
        public static extern void ShowScanQrPopup(string text);

        [DllImport("__Internal")]
        public static extern void CloseScanQrPopup();
    }
}