using Base.States;
using Services.Library;
using Services.Quests;

namespace Infrastructure.States
{
    public class LoadLibraryState : IState
    {
        private readonly ILibraryProvider _libraryProvider;
        private readonly IQuestsService _questsService;
        public IGameStateMachine StateMachine { get; set; }

        public LoadLibraryState(ILibraryProvider libraryProvider, IQuestsService questsService)
        {
            _libraryProvider = libraryProvider;
            _questsService = questsService;
        }

        public async void Enter()
        {
            await _libraryProvider.Load();
            _questsService.Initialize();
            StateMachine.Enter<LoadProgressState>();
        }

        public void Exit()
        {
            
        }
    }
}