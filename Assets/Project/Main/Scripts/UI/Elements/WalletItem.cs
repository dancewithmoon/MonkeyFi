using Services.TonWallet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class WalletItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Image _icon;

        private WalletModel _model;
        
        public void Initialize(WalletModel model)
        {
            _model = model;
        }

        public void Draw()
        {
            _name.text = _model.Name;
            _icon.sprite = _model.Icon;
        }
    }
}