using Base.States;

namespace Base
{
    public class Game
    {
        public IGameStateMachine StateMachine { get; }

        public Game(IGameStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
    }
}