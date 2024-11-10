using Services.Quests;
using TMPro;
using UnityEngine;

namespace UI.Elements
{
    public class QuestItem : BaseItem<QuestModel>
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _reward;
        
        public override void Draw()
        {
            _name.text = Model.Data.Name;
            _reward.text = Model.Data.Reward.ToString();
        }
    }
}