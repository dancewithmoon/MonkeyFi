using Services.Referral;
using TMPro;
using UnityEngine;

namespace UI.Elements
{
    public class FriendItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        private ReferralModel _model;
        
        public void Initialize(ReferralModel model)
        {
            _model = model;
        }

        public void Draw()
        {
            _nameText.text = _model.Name;
        }
    }
}