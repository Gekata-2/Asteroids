using System;
using _Project.Scripts.EnemyAI.StateMachine;
using _Project.Scripts.EnemyAI.StateMachine.States;
using _Project.Scripts.Entities.Asteroids;
using _Project.Scripts.Entities.UFO.Configs;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.Entities.UFO
{
    public class Ufo : EnemyEntity, IDamageVisitable, IDamageVisitor
    {
        public event Action<Ufo> Died;

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
                playerHealth.Accept(this);
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

        public void HandleBullet()
            => _hasBeenHitByBullet = true;

        public void HandleLaser()
            => _hasBeenSweepedByLaser = true;

        public void Accept(IDamageVisitor visitor)
            => visitor.Visit(this);

        public void Visit(PlayerHealth playerHealth) 
            => playerHealth.Die();

        public void Visit(Asteroid asteroid)
        {
        }

        public void Visit(Ufo ufo)
        {
        }
    }
}