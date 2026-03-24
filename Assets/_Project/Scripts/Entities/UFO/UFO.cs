using System;
using _Project.Scripts.EnemyAI.StateMachine;
using _Project.Scripts.EnemyAI.StateMachine.States;
using _Project.Scripts.Entities.UFO.Configs;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons.Laser;
using _Project.Scripts.Player.Weapons.MachineGun;
using UnityEngine;

namespace _Project.Scripts.Entities.UFO
{
    public class UFO : Entity, IDamageble
    {
        public event Action<UFO> Died;

        private StateMachine _stateMachine;

        private bool _isActive;
        private bool _initialized;
        
        private bool _hasBeenHitByBullet;
        private bool _hasBeenSweepedByLaser;

        private EnemyTarget _target;
        
        public float Speed { get; private set; }
        public float SteeringSpeed { get; private set; }

        public float Rotation { get; private set; }
        public Vector2 Position => Rigidbody.position;
        public Vector2 Up => transform.up;


        public void Initialize(UfoConfig ufoConfig, EnemyTarget target)
        {
            InitializeData(ufoConfig);
            Speed = ufoConfig.Movement.Speed;
            SteeringSpeed = ufoConfig.Movement.SteeringSpeed;
            _target = target;
            _initialized = true;
        }

        private void Start()
        {
            _stateMachine = new StateMachine();
            IdleState idleState = new(this);
            ChaseState chaseState = new(_target, this);
            DieState dieState = new(this);

            _stateMachine.AddTransition(idleState, chaseState, new FuncPredicate(() => _initialized));
            _stateMachine.AddAnyTransition(dieState, new FuncPredicate(() => _hasBeenHitByBullet || _hasBeenSweepedByLaser));
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
            if (!_isActive)
                return;

            HandlePositionChanger();
            _stateMachine.FixedUpdate();
        }

        public void SetVelocity(Vector2 velocity)
            => Rigidbody.linearVelocity = velocity;

        public void SetRotation(float rotation)
        {
            Rotation = rotation;
            Rigidbody.SetRotation(Quaternion.Euler(0, 0, Rotation));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out PlayerHealth playerHealth))
                playerHealth.TakeDamage(new Damage(this));
        }

        public void Die()
        {
            Died?.Invoke(this);
            Destroy(gameObject);
        }

        public override void Pause()
        {
            _isActive = false;
            Rigidbody.simulated = false;
        }

        public override void Resume()
        {
            _isActive = true;
            Rigidbody.simulated = true;
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