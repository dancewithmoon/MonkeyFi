using Services.Referral;
using TMPro;
using UnityEngine;

namespace UI.Elements
{
    public class FriendItem : BaseItem<ReferralModel>
    {
        [SerializeField] private TMP_Text _nameText;

        public override void Draw()
        {
            _nameText.text = Model.Name;
        }
    }
}