using Entities.UFO;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyTarget : MonoBehaviour, IEnemyTargetable
    {
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public Vector2 Position
            => _rb.position;
    }
}