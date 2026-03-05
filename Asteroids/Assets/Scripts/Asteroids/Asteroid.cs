using Entities;
using UnityEngine;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : Entity
    {
        private Rigidbody2D _rb;
        private float _speed;
        private Vector2 _moveDirection;
        private Vector2 _velocity;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Initialize(float speed, Vector2 moveDirection)
        {
            _speed = speed;
            _moveDirection = moveDirection.normalized;

            UpdateVelocity();
        }

        private void UpdateVelocity()
            => _velocity = _moveDirection * _speed;

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            _rb.linearVelocity = _velocity;
        }

        public override void Enable()
        {
        }

        public override void Disable()
        {
        }

        public override void SetPosition(Vector3 position)
        {
        }
    }
}