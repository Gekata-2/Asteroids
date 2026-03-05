using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : Entity
    {
        [SerializeField] private float speed;
        [SerializeField] private float rotationSpeed;

        private IInput _input;
        private Rigidbody2D _rb;

        private bool _isMoveForward;

        private float _rotation;

        private Queue<Action> _actionsToPerform;
        private bool _isChangingPosition;

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
            _actionsToPerform = new Queue<Action>();
        }

        private void Start()
        {
            _input.PlayerPerformedMovingForward += Input_OnPlayerPerformedMovingForward;
            _input.PlayerCanceledMovingForward += Input_OnPlayerCanceledMovingForward;
        }

        private void FixedUpdate()
        {
            if (!_actionsToPerform.IsEmpty())
                PerformNextAction();

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

        private void PerformNextAction()
        {
            _actionsToPerform.Dequeue().Invoke();
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

        public override void SetPosition(Vector3 position)
        {
            if (_isChangingPosition)
                return;

            _isChangingPosition = true;
            _rb.interpolation = RigidbodyInterpolation2D.None;
            _actionsToPerform.Enqueue(() => _rb.position = position);
            _actionsToPerform.Enqueue(() =>
            {
                _rb.interpolation = RigidbodyInterpolation2D.Interpolate;
                _isChangingPosition = false;
            });
            //_rb.MovePosition(position);
            // _rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        }
    }
}