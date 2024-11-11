using System.Collections.Generic;
using Services.Library.Quests;
using Services.Quests.Conditions;

namespace Services.Quests
{
    public class QuestsService : IQuestsService
    {
        private readonly IQuestDataProvider _questDataProvider;

        public List<QuestModel> Quests { get; } = new();
        public List<QuestModel> CompletedQuests { get; } = new();
        public List<ConditionModel> CompletedConditions { get; } = new();
        
        public QuestsService(IQuestDataProvider questDataProvider)
        {
            _questDataProvider = questDataProvider;
        }

        public void Initialize()
        {
            foreach (QuestData questData in _questDataProvider.QuestsData.Quests)
            {
                var questModel = new QuestModel(questData);
                questModel.OnConditionCompletedEvent += OnConditionCompleted;
                questModel.OnQuestCompletedEvent += OnQuestCompeted;
                Quests.Add(questModel);
            }
        }

        private void OnConditionCompleted(ConditionModel conditionModel)
        {
            CompletedConditions.Add(conditionModel);
        }

        private void OnQuestCompeted(QuestModel questModel)
        {
            CompletedQuests.Add(questModel);
        }
    }
}