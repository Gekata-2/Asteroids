using UnityEngine;

namespace Entities
{
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
}