using _Project.Scripts.Services.Pause;
using UnityEngine;

namespace _Project.Scripts.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Entity : MonoBehaviour, IPausable
    {
        private RigidBody2DTeleporter _teleporter;
        protected Rigidbody2D Rigidbody;

        public int Score { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        protected void InitializeData(EntityConfig entityData)
        {
            Score = entityData.Score;
        }

        public void SetPosition(Vector3 position)
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

        public abstract void Pause();
        public abstract void Resume();
    }
}