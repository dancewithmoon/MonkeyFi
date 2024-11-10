using Services.Library.Quests;
using UnityEngine;

namespace Services.Quests.Conditions
{
    public class LinkClickConditionModel : ConditionModel
    {
        protected internal LinkClickConditionModel(QuestConditionData data) : base(data)
        {
        }

        public override void Complete() => Application.OpenURL(Data.Link);
    }
}