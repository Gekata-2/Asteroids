using _Project.Scripts.EnemyAI.StateMachine.States;

namespace _Project.Scripts.EnemyAI.StateMachine
{
    public class Transition : ITransition
    {
        public IState To { get; }
        public IPredicate Condition { get; }

        public Transition(IState to, IPredicate condition)
        {
            To = to;
            Condition = condition;
        }
    }
}