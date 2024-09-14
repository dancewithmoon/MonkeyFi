using System;

namespace Services.UserProgress
{
    public interface IUserProgressService
    {
        int ClickerPoints { get; }
        int CurrentEnergy { get; }
        int MaxEnergy { get; }
        int EnergyRechargePerSecond { get; }

        event Action OnProgressLoadedEvent;
        
        void LoadProgress();
    }
}