using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.EnemyAI.StateMachine
{
    public class StateMachine
    {
        private StateNode _current;
        private readonly Dictionary<Type, StateNode> _nodes = new();
        private readonly HashSet<ITransition> _anyTransition = new();

        public void Update()
        {
            ITransition transition = GetTransition();
            if (transition != null) 
                ChangeState(transition.To);

            _current.State?.Update();
        }

        public void FixedUpdate()
        {
            _current.State?.FixedUpdate();
        }

        public void SetState(IState state)
        {
            _current = _nodes[state.GetType()];
            _current.State?.OnEnter();
            Debug.Log($"Entered State : [<color=green>{_current.State?.GetName()}</color>]");
        }

        public void ChangeState(IState state)
        {
            if (state == _current.State)
                return;

            IState previousState = _current.State;
            IState nextState = _nodes[state.GetType()].State;

            previousState?.OnExit();
            nextState.OnEnter();
            Debug.Log(
                $"Changed State: [<color=red>{previousState?.GetName()}</color>] => [<color=green>{nextState.GetName()}</color>]");

            _current = _nodes[state.GetType()];
        }

        private ITransition GetTransition()
        {
            foreach (ITransition transition in _anyTransition)
                if (transition.Condition.Evaluate())
                    return transition;

            foreach (ITransition transition in _current.Transitions)
                if (transition.Condition.Evaluate())
                    return transition;

            return null;
        }

        public void AddTransition(IState from, IState to, IPredicate condition)
            => GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);

        public void AddAnyTransition(IState to, IPredicate condition)
            => _anyTransition.Add(new Transition(GetOrAddNode(to).State, condition));

        private StateNode GetOrAddNode(IState state)
        {
            StateNode node = _nodes.GetValueOrDefault(state.GetType());

            if (node == null)
            {
                node = new StateNode(state);
                _nodes.Add(state.GetType(), node);
            }

            return node;
        }
    }
}