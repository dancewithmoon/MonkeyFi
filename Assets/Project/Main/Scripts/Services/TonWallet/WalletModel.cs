using UnityEngine;

namespace Services.TonWallet
{
    public class WalletModel
    {
        public string AppName { get; }
        public string Name { get; }
        public Sprite Icon { get; }
        
        public WalletModel(string appName, string name, Sprite icon)
        {
            AppName = appName;
            Name = name;
            Icon = icon;
        }
    }
}