using System.Runtime.InteropServices;
using UnityEngine;

namespace Plugins.Telegram
{
    public static class TelegramBridge
    {
        public static TelegramUserDto GetTelegramUserData() => 
            JsonUtility.FromJson<TelegramUserDto>(
                Marshal.PtrToStringAnsi(GetUserData()));

        public static string GetTelegramStartParam() => 
            Marshal.PtrToStringAnsi(GetStartParam());

        [DllImport("__Internal")]
        private static extern System.IntPtr GetUserData();
        
        [DllImport("__Internal")]
        private static extern System.IntPtr GetStartParam();

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