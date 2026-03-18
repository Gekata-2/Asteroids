using System;
using _Project.Scripts.Entities;
using _Project.Scripts.Services;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons.MachineGun
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour, IPausable
    {
        public event Action<Bullet> Collided;

        public struct BulletData
        {
            public float Speed { get; }

            public float LifeTime { get; }

            public Vector2 MoveDirection { get; }

            public BulletData(float speed, Vector2 moveDirection, float lifeTime)
            {
                MoveDirection = moveDirection;
                LifeTime = lifeTime;
                Speed = speed;
            }
        }

        private Rigidbody2D _rb;
        
        private float _speed;
        private Vector2 _direction;
        public float TimeToLive { get; private set; }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rb.linearVelocity = _direction * _speed;
        }

        private void Update()
        {
            if (_rb.simulated)
                TimeToLive -= Time.deltaTime;
        }

        public void Initialize(BulletData bulletData)
        {
            _speed = bulletData.Speed;
            _direction = bulletData.MoveDirection;
            TimeToLive = bulletData.LifeTime;
        }

        public void Pause() 
            => _rb.simulated = false;
        
        public void Resume() 
            => _rb.simulated = true;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageble damageble))
            {
                Collided?.Invoke(this);
                damageble.TakeDamage(new Damage(this));
            }
        }
    }
}