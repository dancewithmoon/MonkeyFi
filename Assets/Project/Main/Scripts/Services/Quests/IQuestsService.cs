using System.Collections.Generic;
using Services.Quests.Conditions;

namespace Services.Quests
{
    public interface IQuestsService
    {
        List<QuestModel> Quests { get; }
        List<QuestModel> CompletedQuests { get; }
        List<ConditionModel> CompletedConditions { get; }

        void Initialize();
        void UpdateProgress(List<int> completedConditions, List<int> completedQuests);
    }
}