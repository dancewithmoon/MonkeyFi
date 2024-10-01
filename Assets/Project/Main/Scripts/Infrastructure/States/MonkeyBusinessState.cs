using Base.States;

namespace Infrastructure.States
{
    public class MonkeyBusinessState : IState
    {
        public IGameStateMachine StateMachine { get; set; }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}