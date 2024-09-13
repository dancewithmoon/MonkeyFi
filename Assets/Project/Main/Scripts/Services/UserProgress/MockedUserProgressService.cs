using System;
using Infrastructure.StaticData.Services;
using UnityEngine;

namespace Services.UserProgress
{
    public class MockedUserProgressService : IUserProgressService
    {
        private readonly IStaticDataService _staticDataService;
        
        public int ClickerPoints { get; private set; }
        public int CurrentEnergy { get; private set; }
        public int MaxEnergy { get; private set; }
        
        public event Action OnProgressLoadedEvent;

        public MockedUserProgressService(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void LoadProgress()
        {
            ClickerPoints = PlayerPrefs.GetInt(nameof(ClickerPoints), 0);
            CurrentEnergy = PlayerPrefs.GetInt(nameof(CurrentEnergy), -1);
            MaxEnergy = PlayerPrefs.GetInt(nameof(MaxEnergy), -1);
            
            if (MaxEnergy < 0)
                MaxEnergy = _staticDataService.GetConfig().DefaultMaxEnergy;

            if (CurrentEnergy < 0)
                CurrentEnergy = MaxEnergy;
            
            OnProgressLoadedEvent?.Invoke();
        }
    }
}