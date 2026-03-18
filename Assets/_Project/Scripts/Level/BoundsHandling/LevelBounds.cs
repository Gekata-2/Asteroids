using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Entities;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Level.BoundsHandling
{
    public class LevelBounds : MonoBehaviour
    {
        public event Action<List<Entity>> EntitiesOutOfBounds;
        public event Action<List<Entity>> EntitiesOutOfOuterBounds;
        public event Action<List<Entity>> EntitiesFirstTimeEnteredLevel;

        [SerializeField] private Vector2 size;
        [SerializeField] private Vector2 outerBoundsSize;
        [SerializeField] private float skinWidth = 0.15f;
        [SerializeField] private bool drawGizmos;

        private EntitiesContainer _entitiesContainer;
        private Bounds _bounds;
        private Bounds _outerBounds;

        public Bounds Bounds => _bounds;
        public float SkinWidth => skinWidth;

        [Inject]
        private void Construct(EntitiesContainer entitiesContainer)
        {
            _entitiesContainer = entitiesContainer;
        }

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

        private void FixedUpdate()
        {
            List<Entity> entitiesOutOfOuterBounds = FindEntitiesOutOfOuterBounds();
            EntitiesOutOfOuterBounds?.Invoke(entitiesOutOfOuterBounds);

            List<Entity> entitiesFirstEnteredLevel = FindEntitiesFirstEnteredLevel();
            EntitiesFirstTimeEnteredLevel?.Invoke(entitiesFirstEnteredLevel);

            List<Entity> entities = FindEntitiesOutOfBounds();
            EntitiesOutOfBounds?.Invoke(entities);
        }

        private Bounds GetBounds(Vector2 boundsSize)
            => new(Vector3.zero, boundsSize);

        private List<Entity> FindEntitiesOutOfBounds()
            => _entitiesContainer.Entities.Where(entity => entity.HasEnteredLevel && IsEntityOutOfBounds(entity))
                .ToList();

        private List<Entity> FindEntitiesOutOfOuterBounds()
            => _entitiesContainer.Entities.Where(IsEntityOutOfOuterBounds).ToList();

        private List<Entity> FindEntitiesFirstEnteredLevel() =>
            _entitiesContainer.Entities.Where(entity => !entity.HasEnteredLevel && !IsEntityOutOfBounds(entity))
                .ToList();

        private bool IsEntityOutOfBounds(Entity entity)
            => !_bounds.Contains(entity.transform.position);

        private bool IsEntityOutOfOuterBounds(Entity entity)
            => !_outerBounds.Contains(entity.transform.position);

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