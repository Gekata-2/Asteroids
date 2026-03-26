using System;
using _Project.Scripts.Entities;
using _Project.Scripts.Entities.Asteroids;
using _Project.Scripts.Entities.UFO;
using _Project.Scripts.Services.Pause;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons.MachineGun
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour, IPausable, IDamageVisitor
    {
        public event Action<Bullet> Collided;

       

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
            if (other.gameObject.TryGetComponent(out IDamageVisitable visitable))
            {
                visitable.Accept(this);
                Collided?.Invoke(this);
            }
        }

        public void Visit(PlayerHealth playerHealth)
        {
        }

        public void Visit(Asteroid asteroid)
        {
            asteroid.HandleBullet();
        }

        public void Visit(UFO ufo)
        {
            ufo.HandleBullet();
        }
    }
}