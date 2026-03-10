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
        [SerializeField] private float speed;
        [SerializeField] private float rotationSpeed;

        private IInput _input;

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
            speed = config.Speed;
            rotationSpeed = config.RotationSpeed;
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
        {
            _isMoveForward = true;
        }

        private void Input_OnPlayerCanceledMovingForward()
        {
            _isMoveForward = false;
        }

        private void Rotate()
        {
            float rotationDelta = -_input.PlayerRotation() * rotationSpeed;
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

            _rb.AddForce(transform.up * speed, ForceMode2D.Force);
        }


        // TODO: пересмотреть

        public override void Enable()
        {
            //  _rb.simulated = true;
            // _actionsToPerform.Enqueue(() => _rb.simulated = true);
        }

        public override void Disable()
        {
            //   _rb.simulated = false;
            //     _actionsToPerform.Enqueue(() => _rb.simulated = false);
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