using System;
using EnemyAI.StateMachine;
using EnemyAI.StateMachine.States;
using UnityEngine;

namespace Entities.UFO
{
    public interface IEnemyTargetable
    {
        public Vector2 Position { get; }
    }

    public class FuncPredicate : IPredicate
    {
        private readonly Func<bool> _func;

        public FuncPredicate(Func<bool> func)
        {
            _func = func;
        }

        public bool Evaluate()
            => _func.Invoke();
    }

    public class UFO : PhysicalEntity
    {
        private StateMachine _stateMachine;
        private IEnemyTargetable _target;
        public Rigidbody2D Rb => _rb;

        public void SetTarget(IEnemyTargetable target)
        {
            _target = target;
        }

        private void Start()
        {
            _stateMachine = new StateMachine();
            IdleState idleState = new IdleState(this);
            ChaseState chaseState = new ChaseState(_target, this);
            _stateMachine.AddTransition(idleState, chaseState, new FuncPredicate(() => true));
            _stateMachine.SetState(idleState);
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }

        public override void Pause()
        {
        }

        public override void Resume()
        {
        }
    }
}