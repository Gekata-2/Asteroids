using Entities;
using UnityEngine;
using Zenject;

namespace Player
{
    public interface IPlayerMovement
    {
        Vector2 Position { get; }
        float Speed { get; }
        float Rotation { get; }
    }

    public class PlayerMovement : PhysicalEntity, IPlayerMovement
    {
        private IInput _input;
        
        private float _speed;
        private float _rotationSpeed;
        private float _rotation;

        private bool _isMoveForward;
        private bool _isChangingPosition;
        
        public Vector2 Position => _rb.position;
        public float Speed => _rb.linearVelocity.magnitude;
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
            _input.PlayerPerformedMovingForward += Input_OnPlayerPerformedMovingForward;
            _input.PlayerCanceledMovingForward += Input_OnPlayerCanceledMovingForward;
        }

        private void OnDestroy()
        {
            _input.PlayerPerformedMovingForward -= Input_OnPlayerPerformedMovingForward;
            _input.PlayerCanceledMovingForward -= Input_OnPlayerCanceledMovingForward;
        }

        private void FixedUpdate()
        {
            if (!_rb.simulated)
                return;

            HandlePositionChanger();

            if (_input == null)
                return;

            Rotate();
            Move();
        }


        private void Input_OnPlayerPerformedMovingForward() 
            => _isMoveForward = true;

        private void Input_OnPlayerCanceledMovingForward() 
            => _isMoveForward = false;

        private void Rotate()
        {
            float rotationDelta = -_input.PlayerRotation() * _rotationSpeed;
            _rotation = GetNewRotation(rotationDelta);

            _rb.SetRotation(Quaternion.Euler(0, 0, _rotation));
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

            _rb.AddForce(transform.up * _speed, ForceMode2D.Force);
        }
        
        public override void Pause() 
            => _rb.simulated = false;

        public override void Resume() 
            => _rb.simulated = true;
    }
}