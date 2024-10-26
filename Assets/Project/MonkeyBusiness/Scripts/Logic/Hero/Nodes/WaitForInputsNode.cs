using MonkeyBusiness.Services.InputService;
using Zenject;

namespace MonkeyBusiness.Logic.Hero.Nodes
{
    public class WaitForInputsNode : HeroNode
    {
        private IInputService _inputService;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }
        
        public override void Enter()
        {
            base.Enter();
            _inputService.OnInputStart += SwitchToMovementState;
        }

        public override void Exit()
        {
            base.Exit();
            _inputService.OnInputStart -= SwitchToMovementState;
        }

        private void SwitchToMovementState() => 
            stateMachine.Enter(HeroStateType.Movement);
    }
}