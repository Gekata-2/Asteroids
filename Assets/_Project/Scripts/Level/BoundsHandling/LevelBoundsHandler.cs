using System;
using _Project.Scripts.Entities;
using UnityEngine;

namespace _Project.Scripts.Level.BoundsHandling
{
    [RequireComponent(typeof(Entity))]
    public class LevelBoundsHandler : MonoBehaviour
    {
        public event Action<Entity> Destroyed;

        private Entity _entity;
        private LevelBounds _levelBounds;
        private bool _hasEnteredLevel;
        private readonly EntityPositionWrapper _positionWrapper = new();

        private void Awake()
        {
            _entity = GetComponent<Entity>();
        }

        public void Initialize(LevelBounds levelBounds)
        {
            _levelBounds = levelBounds;
        }

        private void FixedUpdate()
        {
            Vector2 position = transform.position;

            if (!_hasEnteredLevel && _levelBounds.IsInsideLevel(position))
                _hasEnteredLevel = true;

            if (!_hasEnteredLevel)
                return;

            if (_levelBounds.IsOutsideOfOuterBounds(position))
            {
                HandleOutOfOuterBounds();
                return;
            }

            if (_levelBounds.IsOutsideOfBounds(position)) 
                HandleOutOfBounds();
        }

        private void HandleOutOfBounds()
        {
            _positionWrapper.WrapEntityPosition(_entity, _levelBounds);
        }

        private void HandleOutOfOuterBounds()
        {
            Destroy(_entity.gameObject);
            Destroyed?.Invoke(_entity);
        }
    }
}