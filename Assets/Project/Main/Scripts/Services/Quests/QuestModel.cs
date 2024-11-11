using System;
using System.Collections.Generic;
using System.Linq;
using Services.Library.Quests;
using Services.Quests.Conditions;

namespace Services.Quests
{
    public class QuestModel
    {
        public QuestData Data { get; }
        public List<ConditionModel> Conditions { get; } = new();
        public bool Completed { get; set; }
        
        public event Action<ConditionModel> OnConditionCompletedEvent;
        public event Action<QuestModel> OnQuestCompletedEvent;

        public QuestModel(QuestData data)
        {
            Data = data;
            foreach (QuestConditionData conditionData in data.Conditions)
            {
                var conditionModel = ConditionModel.Create(conditionData);
                conditionModel.OnCompletedEvent += OnConditionCompleted;
                Conditions.Add(conditionModel);
            }
        }

        private void OnConditionCompleted(ConditionModel conditionModel)
        {
            OnConditionCompletedEvent?.Invoke(conditionModel);

            if (AllConditionsCompleted())
                Complete();
        }

        private void Complete()
        {
            Completed = true;
            OnQuestCompletedEvent?.Invoke(this);
        }

        private bool AllConditionsCompleted() => 
            Conditions.All(condition => condition.Completed);
    }
}