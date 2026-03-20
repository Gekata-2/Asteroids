using System;
using System.Collections.Generic;
using _Project.Scripts.Entities.Asteroids.Configs;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons.Laser;
using _Project.Scripts.Player.Weapons.MachineGun;
using UnityEngine;

namespace _Project.Scripts.Entities.Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : PhysicalEntity, IDamageble
    {
        public event Action<Asteroid> CollidedWithBullet;
        public event Action<Asteroid> SweepedByLaser;
        
        public Queue<AsteroidsSplitConfig> SplitChain { get; private set; }

        public void Initialize(AsteroidsInitializationData initializationData)
        {
            InitializeData(initializationData.EntityConfig);
            SplitChain = initializationData.SplitChain;
            Rigidbody.AddTorque(initializationData.Torque);
            Rigidbody.linearVelocity = initializationData.Speed * initializationData.MoveDirection.normalized;
        }

        private void FixedUpdate()
        {
            HandlePositionChanger();
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        public void TakeDamage(Damage damage)
        {
            switch (damage.Source)
            {
                case Bullet:
                    CollidedWithBullet?.Invoke(this);
                    break;
                case Laser:
                    SweepedByLaser?.Invoke(this);
                    break;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out PlayerHealth playerHealth))
                playerHealth.TakeDamage(new Damage(this));
        }

        public override void Pause()
            => Rigidbody.simulated = false;

        public override void Resume()
            => Rigidbody.simulated = true;
    }
}