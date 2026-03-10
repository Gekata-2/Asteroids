using System;
using EnemyAI.StateMachine;
using EnemyAI.StateMachine.States;
using Player;
using Player.Weapons.Laser;
using Player.Weapons.MachineGun;
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

    public class UFO : PhysicalEntity, IDamageble
    {
        public event Action<UFO> Died; 
        
        private StateMachine _stateMachine;
        private IEnemyTargetable _target;
        private bool _initialized;
        private bool _hasBeenHitByBullet;
        private bool _hasBeenSweepedByLaser;
        private bool _isActive;
        public float Rotation { get; set; }
        public Rigidbody2D Rb => _rb;

        public override void InitializeData(EntityData entityData)
        {
            base.InitializeData(entityData);
            _initialized = true;
        }

        public void SetTarget(IEnemyTargetable target)
        {
            _target = target;
        }

        private void Start()
        {
            _stateMachine = new StateMachine();
            IdleState idleState = new IdleState(this);
            ChaseState chaseState = new ChaseState(_target, this);
            DieState dieState = new DieState(this);
            DestroyState destroyState = new DestroyState(this);
          
            _stateMachine.AddTransition(idleState, chaseState, new FuncPredicate(() => _initialized));
            _stateMachine.AddAnyTransition(dieState, new FuncPredicate(() => _hasBeenHitByBullet));
            _stateMachine.AddAnyTransition(destroyState, new FuncPredicate(() => _hasBeenSweepedByLaser));
            _stateMachine.SetState(idleState);
          
            _isActive = true;
        }

        private void Update()
        {
            if (_isActive)
                _stateMachine.Update();
        }

        private void FixedUpdate()
        {
            if (_isActive)
                _stateMachine.FixedUpdate();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out PlayerHealth playerHealth))
                playerHealth.TakeDamage(new Damage(this));
        }

        public void NotifyAboutDeath()
        {
            Died?.Invoke(this);
            Destroy(gameObject);
        }

        public override void Pause()
        {
            _isActive = false;
            _rb.simulated = false;
        }

        public override void Resume()
        {
            _isActive = true;
            _rb.simulated = true;
        }

        public void TakeDamage(Damage damage)
        {
            switch (damage.Source)
            {
                case Bullet:
                    _hasBeenHitByBullet = true;
                    break;
                case Laser:
                    _hasBeenSweepedByLaser = true;
                    break;
            }
        }
    }
}