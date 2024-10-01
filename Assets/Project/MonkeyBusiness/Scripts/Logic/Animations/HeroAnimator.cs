using System;
using UnityEngine;

namespace MonkeyBusiness.Logic.Animations
{
    public class HeroAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int IdleStateHash = Animator.StringToHash("Idle");
        private static readonly int WalkingStateHash = Animator.StringToHash("Run");

        private static readonly int VelocityParamHash = Animator.StringToHash("Velocity");

        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;
        
        public event Action<AnimatorState> OnStateEntered;
        public event Action<AnimatorState> OnStateExited;

        public event Action OnMiningHit;
        public event Action OnOpenChest;

        public AnimatorState State { get; private set; }

        private void Update()
        {
            float velocity = _characterController.velocity.magnitude;
            if (velocity < 0.001f)
                velocity = 0;
            
            _animator.SetFloat(VelocityParamHash, velocity);
        }

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            OnStateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash) =>
            OnStateExited?.Invoke(StateFor(stateHash));

        private AnimatorState StateFor(int stateHash)
        {
            if (stateHash == IdleStateHash)
                return AnimatorState.Idle;
            
            if (stateHash == WalkingStateHash)
                return AnimatorState.Walking;

            return AnimatorState.Unknown;
        }

        #region AnimatorEvents
        
        public void InvokeMiningHit() => OnMiningHit?.Invoke();
        public void InvokeOpenChest() => OnOpenChest?.Invoke();

        #endregion
    }
}