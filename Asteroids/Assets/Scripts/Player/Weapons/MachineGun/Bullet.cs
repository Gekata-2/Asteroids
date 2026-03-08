using System;
using Services;
using UnityEngine;

namespace Player.Weapons.MachineGun
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour, IPausable
    {
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
        
        private float _speed;
        private Rigidbody2D _rb;
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

        public void SetSpeed(float value)
            => _speed = value;

        public void SetDirection(Vector2 dir)
            => _direction = dir.normalized;

        public void Initialize(BulletData bulletData)
        {
            _speed = bulletData.Speed;
            _direction = bulletData.MoveDirection;
            TimeToLive = bulletData.LifeTime;
        }

        public void Pause() => _rb.simulated = false;
        public void Resume() => _rb.simulated = true;
    }
}