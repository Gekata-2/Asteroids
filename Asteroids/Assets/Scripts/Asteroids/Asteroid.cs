using System;
using System.Collections.Generic;
using Player;
using Player.Weapons.MachineGun;
using Services;
using UnityEngine;

namespace Asteroids
{
    interface IDamageble
    {
        void TakeDamage(Damage damage);
    }

    public class Damage
    {
        public Damage(object source)
        {
            Source = source;
        }

        public object Source { get; }
    }


    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : PhysicalEntity, IDamageble, IPausable
    {
        public event Action<Asteroid> CollidedWithBullet;

        private float _speed;
        private Vector2 _moveDirection;
        private Vector2 _velocity;
        private Queue<AsteroidsChainData> _splitChain;
        public Queue<AsteroidsChainData> SplitChain => _splitChain;

        public void Initialize(float speed, Vector2 moveDirection, Queue<AsteroidsChainData> splitChain)
        {
            _speed = speed;
            _moveDirection = moveDirection.normalized;
            _splitChain = splitChain;
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
            {
                CollidedWithBullet?.Invoke(this);
            }
        }

        public void Pause()
        {
            _rb.simulated = false;
        }

        public void Resume()
        {
            _rb.simulated = true;
        }
    }
}