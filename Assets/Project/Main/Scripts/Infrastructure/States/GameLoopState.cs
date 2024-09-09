using Base.States;

namespace Main.Infrastructure.States
{
    public class GameLoopState : IState
    {
        public IGameStateMachine StateMachine { get; set; }

        public void Exit()
        {
        }

        public void Enter()
        {
        }
    }
}