using _Project.Scripts.EnemyAI.StateMachine.States;

namespace _Project.Scripts.EnemyAI.StateMachine
{
    public interface ITransition
    {
        IState To { get; }
        IPredicate Condition { get; }
    }
}