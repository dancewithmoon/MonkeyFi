using System;
using Services.Library.Quests;

namespace Services.Quests.Conditions
{
    public abstract class ConditionModel
    {
        public QuestConditionData Data { get; }
        
        public bool Completed { get; set; }
        
        public event Action<ConditionModel> OnCompletedEvent;

        protected ConditionModel(QuestConditionData data)
        {
            Data = data;
        }

        public abstract void StartCompletion();

        protected void Complete()
        {
            Completed = true;
            OnCompletedEvent?.Invoke(this);
        }

        public static ConditionModel Create(QuestConditionData data)
        {
            if (data.Type == ConditionType.LinkClick)
                return new LinkClickConditionModel(data);

            throw new Exception($"{nameof(ConditionModel)}.{nameof(Create)} error: Wrong condition type");
        }
    }
}