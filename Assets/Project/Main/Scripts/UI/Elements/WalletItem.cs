using Services.TonWallet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class WalletItem : BaseItem<WalletModel>
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Image _icon;

        public override void Draw()
        {
            _name.text = Model.Name;
            _icon.sprite = Model.Icon;
        }
    }
}