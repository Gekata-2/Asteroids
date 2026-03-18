using UnityEngine;

namespace _Project.Scripts.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PhysicalEntity : Entity
    {
        private RigidBody2DTeleporter _teleporter;
        protected Rigidbody2D Rigidbody;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }
        
        public override void SetPosition(Vector3 position)
        {
            if (_teleporter != null)
                return;

            _teleporter = new RigidBody2DTeleporter(position, Rigidbody);
        }

        protected void HandlePositionChanger()
        {
            if (_teleporter == null)
                return;

            _teleporter.PerformNext();
            if (_teleporter.IsFinished)
                _teleporter = null;
        }
    }
}