using System;

namespace Models
{
    public class ClickerModel
    {
        public int Points { get; private set; }
        public int CurrentEnergy { get; private set; }
        public int MaxEnergy { get; private set; }
        
        public event Action OnStateChangedEvent;

        public void UpdateValues(int points, int currentEnergy, int maxEnergy)
        {
            Points = points;
            CurrentEnergy = currentEnergy;
            MaxEnergy = maxEnergy;
        }
        
        public void Click()
        {
            if(CurrentEnergy == 0)
                return;
            
            Points++;
            CurrentEnergy--;
            OnStateChangedEvent?.Invoke();
        }
    }
}