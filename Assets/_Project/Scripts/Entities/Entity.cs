using _Project.Scripts.Services.Pause;
using UnityEngine;

namespace _Project.Scripts.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Entity : MonoBehaviour, IPausable
    {
        protected Rigidbody2D Rigidbody;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }
        
        public void SetPositionImmediate(Vector2 position)
        {
            transform.position = position;
            Physics2D.SyncTransforms();
        }

        public abstract void Pause();
        public abstract void Resume();
    }
}