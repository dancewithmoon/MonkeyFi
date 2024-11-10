using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.States;
using Services.Library.Quests;
using Services.Quests.Conditions;
using UnityEngine;

namespace Services.Quests
{
    public class QuestsService : IQuestsService, IPreloadedInLoadMenu
    {
        private readonly IQuestDataProvider _questDataProvider;

        public List<QuestModel> Quests { get; } = new();
        public List<QuestModel> CompletedQuests { get; } = new();
        public List<ConditionModel> CompletedConditions { get; } = new();
        
        public QuestsService(IQuestDataProvider questDataProvider)
        {
            _questDataProvider = questDataProvider;
        }

        public Task Preload()
        {
            foreach (QuestData questData in _questDataProvider.QuestsData.Quests)
            {
                var questModel = new QuestModel(questData);
                questModel.OnConditionCompletedEvent += OnConditionCompleted;
                questModel.OnQuestCompletedEvent += OnQuestCompeted;
                Quests.Add(questModel);
            }
            return Task.CompletedTask;
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