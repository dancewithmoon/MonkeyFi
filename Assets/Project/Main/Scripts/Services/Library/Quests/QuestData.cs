namespace Services.Library.Quests
{
    public class QuestsData
    {
        public QuestData[] Quests { get; }

        public QuestsData(QuestData[] quests)
        {
            Quests = quests;
        }
    }

    public class QuestData
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string IconUrl { get; }
        public int Reward { get; }
        public QuestConditionData[] Conditions { get; }

        public QuestData(int id, string name, string description, string iconUrl, int reward, QuestConditionData[] conditions)
        {
            Id = id;
            Name = name;
            Description = description;
            IconUrl = iconUrl;
            Reward = reward;
            Conditions = conditions;
        }
    }

    public class QuestConditionData
    {
        public int Id { get; }
        public string Name { get; }
        public string Type { get; }
        public string IconUrl { get; }
        public string Link { get; }
        
        public QuestConditionData(int id, string name, string type, string iconUrl, string link)
        {
            Id = id;
            Name = name;
            Type = type;
            IconUrl = iconUrl;
            Link = link;
        }
    }
}