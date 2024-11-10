using System.Threading.Tasks;
using Infrastructure.States;
using Newtonsoft.Json;
using PlayFab.ClientModels;
using Services.Library.Config;
using Services.Library.Quests;
using Utils;

namespace Services.Library
{
    public class PlayfabLibraryProvider : IConfigProvider, IQuestDataProvider, IPreloadedAfterAuthorization
    {
        public ConfigData Config { get; private set; }
        public QuestsData QuestsData { get; private set; }
        
        public async Task Preload()
        {
            GetTitleDataResult result = await PlayFabClientAsyncAPI.GetTitleData(new GetTitleDataRequest());
            Config = JsonConvert.DeserializeObject<ConfigData>(result.Data["Config"]);
            QuestsData = JsonConvert.DeserializeObject<QuestsData>(result.Data["Quests"]);
        }
    }
}