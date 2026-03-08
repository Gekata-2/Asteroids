using System;
using System.Collections.Generic;
using Player;
using Player.Weapons.MachineGun;
using UnityEngine;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : PhysicalEntity, IDamageble
    {
        public event Action<Asteroid> CollidedWithBullet;

        private float _speed;
        private Vector2 _moveDirection;
        private Vector2 _velocity;
        public Queue<AsteroidsChainData> SplitChain { get; private set; }

        public void Initialize(float speed, Vector2 moveDirection, Queue<AsteroidsChainData> splitChain)
        {
            _speed = speed;
            _moveDirection = moveDirection.normalized;
            SplitChain = splitChain;
            UpdateVelocity();
        }

        private void UpdateVelocity()
            => _velocity = _moveDirection * _speed;

        private void FixedUpdate()
        {
            HandlePositionChanger();
            Move();
        }

        private void Move()
        {
            _rb.linearVelocity = _velocity;
        }

        public void Die()
        {
            Destroy(gameObject);
        }


        public override void Enable()
        {
        }

        public override void Disable()
        {
        }

        public void TakeDamage(Damage damage)
        {
            if (damage.Source is Bullet)
                CollidedWithBullet?.Invoke(this);
        }

        public void AddTorque(float value)
        {
            _rb.AddTorque(value);
        }

        public override void Pause()
        {
            _rb.simulated = false;
        }

        public override void Resume()
        {
            _rb.simulated = true;
        }
    }
}