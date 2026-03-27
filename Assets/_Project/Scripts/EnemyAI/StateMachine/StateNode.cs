using System.Collections.Generic;
using _Project.Scripts.EnemyAI.StateMachine.States;

namespace _Project.Scripts.EnemyAI.StateMachine
{
    public class StateNode
    {
        public IState State { get; }
        public HashSet<ITransition> Transitions { get; }

        public StateNode(IState state)
        {
            State = state;
            Transitions = new HashSet<ITransition>();
        }

        public void AddTransition(IState to, IPredicate condition)
            => Transitions.Add(new Transition(to, condition));
    }
}