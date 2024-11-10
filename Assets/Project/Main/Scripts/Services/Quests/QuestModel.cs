using System.Collections.Generic;
using Services.Library.Quests;
using Services.Quests.Conditions;

namespace Services.Quests
{
    public class QuestModel
    {
        public QuestData Data { get; }
        public List<ConditionModel> Conditions { get; } = new();

        public QuestModel(QuestData data)
        {
            Data = data;
            foreach (QuestConditionData conditionData in data.Conditions)
            {
                Conditions.Add(ConditionModel.Create(conditionData));
            }
        }
    }
}