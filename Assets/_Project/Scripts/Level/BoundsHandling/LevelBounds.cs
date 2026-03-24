using UnityEngine;

namespace _Project.Scripts.Level.BoundsHandling
{
    public class LevelBounds : MonoBehaviour
    {
        [SerializeField] private Vector2 size;
        [SerializeField] private Vector2 outerBoundsSize;
        [SerializeField] private float skinWidth = 0.15f;
        [SerializeField] private bool drawGizmos;

        private Bounds _bounds;
        private Bounds _outerBounds;

        public Bounds Bounds => _bounds;
        public float SkinWidth => skinWidth;
        
        private void Start()
        {
            UpdateBounds();
        }

        private void OnValidate()
        {
            UpdateBounds();
        }

        private void UpdateBounds()
        {
            _bounds = GetBounds(size);
            _outerBounds = GetBounds(outerBoundsSize);
        }

        private Bounds GetBounds(Vector2 boundsSize)
            => new(Vector3.zero, boundsSize);

        public bool IsOutsideOfBounds(Vector2 position)
            => !_bounds.Contains(position);

        public bool IsOutsideOfOuterBounds(Vector2 position)
            => !_outerBounds.Contains(position);

        public bool IsInsideLevel(Vector2 position)
            => _bounds.Contains(position);


        private void OnDrawGizmos()
        {
            if (!drawGizmos)
                return;

            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(_outerBounds.center, _outerBounds.size);

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(_bounds.center, _bounds.size);

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_bounds.center, _bounds.size - Vector3.one * skinWidth * 2f);
        }
    }
}