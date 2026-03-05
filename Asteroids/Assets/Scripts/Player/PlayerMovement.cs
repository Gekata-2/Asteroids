using System;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float rotationSpeed;

        private IInput _input;
        private Rigidbody2D _rb;

        private bool _isMoveForward;

        private float _rotation;

        [Inject]
        public void Construct(IInput input, PlayerConfig config)
        {
            _input = input;
            speed = config.Speed;
            rotationSpeed = config.RotationSpeed;
        }
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _input.PlayerPerformedMovingForward += Input_OnPlayerPerformedMovingForward;
            _input.PlayerCanceledMovingForward += Input_OnPlayerCanceledMovingForward;
        }

        private void Input_OnPlayerPerformedMovingForward()
        {
            _isMoveForward = true;
        }

        private void Input_OnPlayerCanceledMovingForward()
        {
            _isMoveForward = false;
        }

        private void FixedUpdate()
        {
            if (_input == null)
                return;

            Rotate();
            Move();
        }

        private void Rotate()
        {
            float rotationDelta = -_input.PlayerRotation() * rotationSpeed;
            _rotation += rotationDelta;
            _rb.SetRotation(Quaternion.Euler(0, 0, _rotation));
        }

        private void Move()
        {
            if (!_isMoveForward)
                return;

            _rb.AddForce(transform.up * speed, ForceMode2D.Force);
        }
    }
}