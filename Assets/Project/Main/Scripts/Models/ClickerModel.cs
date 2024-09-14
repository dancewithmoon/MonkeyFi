using System;
using UnityEngine;

namespace Models
{
    public class ClickerModel
    {
        public int Points { get; private set; }
        public int CurrentEnergy { get; private set; }
        public int MaxEnergy { get; private set; }
        public int EnergyRefillPerSecond { get; private set; }
        public DateTime LastEnergyUpdateTime { get; private set; }
        public bool NeedEnergyRecharge => CurrentEnergy < MaxEnergy;
        public event Action OnStateChangedEvent;

        public void UpdateValues(int points, int currentEnergy, int maxEnergy, int energyRefillPerSecond)
        {
            Points = points;
            CurrentEnergy = currentEnergy;
            MaxEnergy = maxEnergy;
            EnergyRefillPerSecond = energyRefillPerSecond;
            LastEnergyUpdateTime = DateTime.Now;
        }
        
        public void Click()
        {
            if(CurrentEnergy == 0)
                return;
            
            Points++;
            CurrentEnergy--;
            LastEnergyUpdateTime = DateTime.Now;
            OnStateChangedEvent?.Invoke();
        }

        public void RechargeEnergy()
        {
            DateTime now = DateTime.Now;
            TimeSpan timePassed = now - LastEnergyUpdateTime;
            int energyRefilled = (int)(EnergyRefillPerSecond * timePassed.TotalSeconds);
            if(energyRefilled == 0)
                return;
            
            CurrentEnergy = Mathf.Clamp(CurrentEnergy + energyRefilled, 0, MaxEnergy);
            LastEnergyUpdateTime = now;
            OnStateChangedEvent?.Invoke();
        }
    }
}