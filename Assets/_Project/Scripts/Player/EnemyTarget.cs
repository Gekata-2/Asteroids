using UnityEngine;

namespace _Project.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyTarget : MonoBehaviour
    {
        public Vector2 Position
            => transform.position;
    }
}