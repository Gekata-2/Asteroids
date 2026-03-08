using System;
using System.Collections.Generic;
using Entities;
using ModestTree;
using Services;
using UnityEngine;
using Zenject;

namespace Player
{
    public interface IPlayerMovement : IPausable
    {
        Vector2 Position { get; }
        float Speed { get; }
        float Rotation { get; }
    }

    class RigidBody2DPositionChanger
    {
        private readonly Queue<Action> _actions;

        public RigidBody2DPositionChanger(Vector3 position, Rigidbody2D rb)
        {
            _actions = new Queue<Action>();
            _actions.Enqueue(() => rb.interpolation = RigidbodyInterpolation2D.None);
            _actions.Enqueue(() => rb.position = position);
            _actions.Enqueue(() => rb.interpolation = RigidbodyInterpolation2D.Interpolate);
        }

        public void PerformNext()
        {
            if (_actions.IsEmpty())
                return;

            _actions.Dequeue().Invoke();
        }

        public bool IsFinished
            => _actions.IsEmpty();
    }

    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PhysicalEntity : Entity
    {
        private RigidBody2DPositionChanger _positionChanger;
        protected Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public override void Enable()
        {
        }

        public override void Disable()
        {
        }

        public override void SetPosition(Vector3 position)
        {
            if (_positionChanger != null)
                return;

            _positionChanger = new RigidBody2DPositionChanger(position, _rb);
        }

        protected void HandlePositionChanger()
        {
            if (_positionChanger == null)
                return;

            _positionChanger.PerformNext();
            if (_positionChanger.IsFinished)
                _positionChanger = null;
        }
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