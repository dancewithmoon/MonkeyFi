using Services.Library.Quests;

namespace Services.Quests
{
    public class QuestModel
    {
        public QuestData Data { get; }

        public QuestModel(QuestData data)
        {
            Data = data;
        }
    }
}