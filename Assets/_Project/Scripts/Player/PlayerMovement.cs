using System;
using _Project.Scripts.Entities;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Player
{
    public class PlayerMovement : PhysicalEntity, IPlayerMovement
    {
        private IInput _input;
        
        private float _speed;
        private float _rotationSpeed;
        private float _rotation;

        private bool _isMoveForward;
        private bool _isChangingPosition;

        public event Action PositionChanged;
        public event Action SpeedChanged;
        public event Action RotationChanged;
        
        public Vector2 Position => Rigidbody.position;
        public float Speed => Rigidbody.linearVelocity.magnitude;
        public float Rotation => _rotation;
        
        [Inject]
        public void Construct(IInput input, PlayerConfig config)
        {
            _input = input;
            _speed = config.Speed;
            _rotationSpeed = config.RotationSpeed;
        }

        private void Start()
        {
            _input.PlayerPerformedMovingForward += OnPlayerPerformedMovingForward;
            _input.PlayerCanceledMovingForward += OnPlayerCanceledMovingForward;
        }

        private void OnDestroy()
        {
            _input.PlayerPerformedMovingForward -= OnPlayerPerformedMovingForward;
            _input.PlayerCanceledMovingForward -= OnPlayerCanceledMovingForward;
        }

        private void FixedUpdate()
        {
            if (!Rigidbody.simulated)
                return;
            
            SpeedChanged?.Invoke();
            PositionChanged?.Invoke();
            
            HandlePositionChanger();

            if (_input == null)
                return;

            Rotate();
            Move();
        }

        private void OnPlayerPerformedMovingForward() 
            => _isMoveForward = true;

        private void OnPlayerCanceledMovingForward() 
            => _isMoveForward = false;

        private void Rotate()
        {
            float rotationDelta = -_input.PlayerRotation() * _rotationSpeed * Time.fixedDeltaTime;
            _rotation = GetNewRotation(rotationDelta);

            Rigidbody.SetRotation(Quaternion.Euler(0, 0, _rotation));
            RotationChanged?.Invoke();
        }

        private float GetNewRotation(float rotationDelta)
        {
            float newRotation = _rotation + rotationDelta;
            newRotation %= 360f;
            if (newRotation < 0f)
                newRotation = 360f + newRotation;
            return newRotation;
        }

        private void Move()
        {
            if (!_isMoveForward)
                return;

            Rigidbody.AddForce(transform.up * _speed, ForceMode2D.Force);
        }
        
        public override void Pause() 
            => Rigidbody.simulated = false;

        public override void Resume() 
            => Rigidbody.simulated = true;
    }
}