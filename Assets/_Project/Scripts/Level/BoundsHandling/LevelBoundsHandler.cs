using System;
using _Project.Scripts.Entities;
using UnityEngine;

namespace _Project.Scripts.Level.BoundsHandling
{
    [RequireComponent(typeof(Entity))]
    public class LevelBoundsHandler : MonoBehaviour
    {
        public event Action<Entity> CrossedBounds;
        public event Action<Entity> CrossedOuterBounds;

        private Entity _entity;
        private LevelBounds _levelBounds;
        private bool _hasEnteredLevel = false;
        private IPositionWrapper _positionWrapper;

        private void Awake()
        {
            _entity = GetComponent<Entity>();
        }

        public void Initialize(LevelBounds levelBounds, IPositionWrapper positionWrapper)
        {
            _levelBounds = levelBounds;
            _positionWrapper = positionWrapper;
        }

        private void FixedUpdate()
        {
            Vector2 position = transform.position;

            if (!_hasEnteredLevel && _levelBounds.IsInsideLevel(position))
                _hasEnteredLevel = true;
            else
                return;

            if (_levelBounds.IsOutsideOfOuterBounds(position))
            {
                CrossedOuterBounds?.Invoke(_entity);
                return;
            }

            if (_levelBounds.IsOutsideOfBounds(position))
            {
                CrossedBounds?.Invoke(_entity);
            }
        }
    }
}