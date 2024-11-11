using System.Collections.Generic;

namespace Services.Quests
{
    public interface IQuestsService
    {
        List<QuestModel> Quests { get; }
        void Initialize();
    }
}