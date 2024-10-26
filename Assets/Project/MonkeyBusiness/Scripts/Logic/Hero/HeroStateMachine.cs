using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace MonkeyBusiness.Logic.Hero
{
    public class HeroStateMachine : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<HeroStateType, HeroState> _states;
        
        private HeroState _activeState;
        
        public void Enter(HeroStateType stateType)
        {
            HeroState newState = ChangeState(stateType);
            newState.Enter();
        }

        private HeroState ChangeState(HeroStateType stateType)
        {
            if (_activeState != null)
                _activeState.Exit();
            
            HeroState newState = GetState(stateType);
            _activeState = newState;
            return newState;
        }
        
        private HeroState GetState(HeroStateType stateType) => _states[stateType];
    }
}