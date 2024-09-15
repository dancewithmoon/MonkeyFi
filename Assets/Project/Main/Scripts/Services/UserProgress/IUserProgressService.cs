using System;

namespace Services.UserProgress
{
    public interface IUserProgressService
    {
        event Action OnProgressLoadedEvent;
        
        void LoadProgress();
        void SaveProgress();
    }
}