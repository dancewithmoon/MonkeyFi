using System.Threading.Tasks;
using Services.Library.Config;
using Services.Library.Quests;

namespace Services.Library
{
    public interface ILibraryProvider : IConfigProvider, IQuestDataProvider
    {
        Task Load();
    }
}