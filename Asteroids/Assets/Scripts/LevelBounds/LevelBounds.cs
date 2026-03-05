using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using ModestTree;
using UnityEngine;
using Zenject;

namespace LevelBounds
{
    public class LevelBounds : MonoBehaviour
    {
        public event Action<List<Entity>> EntitiesOutOfBounds;

        [SerializeField] private float size = 3f;
        [SerializeField] private float skinWidth = 0.15f;
        [SerializeField] private bool drawGizmos;

        private EntitiesContainer _entitiesContainer;
        private Bounds _bounds;

        public Bounds Bounds => _bounds;
        public float SkinWidth => skinWidth;

        [Inject]
        private void Construct(EntitiesContainer entitiesContainer)
        {
            _entitiesContainer = entitiesContainer;
        }

        private void Start()
        {
            _bounds = new Bounds(Vector3.zero, Vector3.one * size);
        }

        private void OnValidate()
        {
            _bounds = new Bounds(Vector3.zero, Vector3.one * size);
        }

        private void FixedUpdate()
        {
            List<Entity> entities = FindEntitiesOutOfBounds();
            if (!entities.IsEmpty())
                EntitiesOutOfBounds?.Invoke(entities);

            //  WrapEntitiesPositions(entities);
        }

        private void WrapEntitiesPositions(List<Entity> entities)
        {
            foreach (Entity entity in entities)
                WrapEntityPosition(entity);
        }

        private void WrapEntityPosition(Entity entity)
        {
            Vector3 position = entity.transform.position;
            Vector3 newPosition = position;
            Vector3 invertedPosition = -position;
            if (invertedPosition.x > _bounds.extents.x || invertedPosition.x < -_bounds.extents.x)
            {
                newPosition.x = Mathf.Clamp(invertedPosition.x, -_bounds.extents.x + skinWidth,
                    _bounds.extents.x - skinWidth);
            }

            Debug.LogWarning(newPosition);
            entity.Disable();
            entity.SetPosition(newPosition);
            entity.Enable();
        }


        private List<Entity> FindEntitiesOutOfBounds()
            => _entitiesContainer.Entities.Where(IsEntityOutOfBounds).ToList();

        private bool IsEntityOutOfBounds(Entity entity)
            => !_bounds.Contains(entity.transform.position);

        private void OnDrawGizmos()
        {
            if (!drawGizmos)
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(_bounds.center, _bounds.size);

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_bounds.center, _bounds.size - Vector3.one * skinWidth * 2f);
        }
    }
}