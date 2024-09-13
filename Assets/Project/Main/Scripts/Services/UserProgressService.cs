using System;

namespace Services
{
    public class UserProgressService
    {
        public int Counter { get; private set; }
        
        public event Action OnCounterUpdateEvent;
        
        public void IncreaseCounter()
        {
            Counter++;
            OnCounterUpdateEvent?.Invoke();
        }
    }
}