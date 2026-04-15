using UnityEngine;

namespace _Project.Scripts.Level.BoundsHandling
{
    public class LevelBounds : MonoBehaviour
    {
        [SerializeField] private Vector2 _size;
        [SerializeField] private float _skinWidth = 0.15f;
        [SerializeField] private bool _drawGizmos;

        private Bounds _bounds;
        
        public Bounds Bounds => _bounds;
        public float SkinWidth => _skinWidth;
        
        private void Start()
        {
            UpdateBounds();
        }

        private void OnValidate()
        {
            UpdateBounds();
        }

        private void UpdateBounds() 
            => _bounds = GetBounds(_size);

        private Bounds GetBounds(Vector2 boundsSize)
            => new(Vector3.zero, boundsSize);

        public bool IsOutsideOfBounds(Vector2 position)
            => !_bounds.Contains(position);
        
        public bool IsInsideLevel(Vector2 position)
            => _bounds.Contains(position);


        private void OnDrawGizmos()
        {
            if (!_drawGizmos)
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(_bounds.center, _bounds.size);

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_bounds.center, _bounds.size - Vector3.one * _skinWidth * 2f);
        }
    }
}