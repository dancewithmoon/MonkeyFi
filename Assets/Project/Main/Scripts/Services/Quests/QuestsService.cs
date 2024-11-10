using System.Collections.Generic;
using System.Threading.Tasks;
using Base.States;
using Services.Library.Quests;

namespace Services.Quests
{
    public class QuestsService : IQuestsService, IPreloadedInLoadMenu
    {
        private readonly IQuestDataProvider _questDataProvider;

        public List<QuestModel> Quests { get; } = new();
        
        public QuestsService(IQuestDataProvider questDataProvider)
        {
            _questDataProvider = questDataProvider;
        }

        public Task Preload()
        {
            foreach (QuestData questData in _questDataProvider.QuestsData.Quests)
            {
                Quests.Add(new QuestModel(questData));
            }
            return Task.CompletedTask;
        }
    }
}