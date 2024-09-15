using System;
using UnityEngine;

namespace Models
{
    public class ClickerModel
    {
        public int Points { get; private set; }
        public int CurrentEnergy { get; private set; }
        public int MaxEnergy { get; private set; }
        public int EnergyRechargePerSecond { get; private set; }
        public bool NeedEnergyRecharge => CurrentEnergy < MaxEnergy;
        private DateTime LastEnergyUpdateTime { get; set; }
        
        public event Action OnStateChangedEvent;

        public void UpdateValues(int points, int currentEnergy, int maxEnergy, int energyRechargePerSecond)
        {
            Points = points;
            CurrentEnergy = currentEnergy;
            MaxEnergy = maxEnergy;
            EnergyRechargePerSecond = energyRechargePerSecond;
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
            int energyRefilled = (int)(EnergyRechargePerSecond * timePassed.TotalSeconds);
            if(energyRefilled == 0)
                return;
            
            CurrentEnergy = Mathf.Clamp(CurrentEnergy + energyRefilled, 0, MaxEnergy);
            LastEnergyUpdateTime = now;
            OnStateChangedEvent?.Invoke();
        }
    }
}