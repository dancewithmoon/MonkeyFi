using System;
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
        [SerializeField] private Button _button;

        public event Action<WalletModel> OnClickEvent;
        
        private void OnEnable() => 
            _button.onClick.AddListener(OnClick);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnClick);

        public override void Draw()
        {
            _name.text = Model.Name;
            _icon.sprite = Model.Icon;
        }

        private void OnClick() => 
            OnClickEvent?.Invoke(Model);
    }
}