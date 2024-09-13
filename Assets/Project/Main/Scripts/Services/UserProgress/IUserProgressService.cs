using System;

namespace Services.UserProgress
{
    public interface IUserProgressService
    {
        int ClickerPoints { get; }
        int CurrentEnergy { get; }
        int MaxEnergy { get; }
        
        event Action OnProgressLoadedEvent;
        
        void LoadProgress();
    }
}