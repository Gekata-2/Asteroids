using System;
using _Project.Scripts.EnemyAI.StateMachine;
using _Project.Scripts.Entities.Asteroids;
using _Project.Scripts.Entities.UFO.Configs;
using UnityEngine;

namespace _Project.Scripts.Entities.UFO
{
    public class Ufo : EnemyEntity, IDamageVisitable, IDamageVisitor
    {
        public event Action<Ufo> Died;

        private StateMachine _stateMachine;
        private bool _isActive;

        public float Speed { get; private set; }
        public float SteeringSpeed { get; private set; }

        public float Rotation { get; private set; }
        public Vector2 Position => Rigidbody.position;
        public Vector2 Up => transform.up;
        public bool Initialized { get; private set; }
        public bool HasBeenHitByBullet { get; private set; }
        public bool HasBeenSweepedByLaser { get; private set; }


        public void Initialize(UfoConfig ufoConfig)
        {
            InitializeData(ufoConfig);
            Speed = ufoConfig.Movement.Speed;
            SteeringSpeed = ufoConfig.Movement.SteeringSpeed;
            Initialized = true;
        }

        private void Start()
        {
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
            
            _stateMachine.FixedUpdate();
        }

        public void SetBehaviour(StateMachine stateMachine)
            => _stateMachine = stateMachine;

        public void SetVelocity(Vector2 velocity)
            => Rigidbody.linearVelocity = velocity;

        public void SetRotation(float rotation)
        {
            Rotation = rotation;
            Rigidbody.SetRotation(Quaternion.Euler(0, 0, Rotation));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageVisitable visitable))
                visitable.Accept(this);
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
            => HasBeenHitByBullet = true;

        public void HandleLaser()
            => HasBeenSweepedByLaser = true;

        public void Accept(IDamageVisitor visitor)
            => visitor.Visit(this);

        public void Visit(Player.Player player)
            => player.Die();

        public void Visit(Asteroid asteroid)
        {
        }

        public void Visit(Ufo ufo)
        {
        }
    }
}