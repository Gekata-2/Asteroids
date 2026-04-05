using _Project.Scripts.Entities;
using _Project.Scripts.Entities.Asteroids.Pools;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Level.BoundsHandling
{
    [RequireComponent(typeof(Entity))]
    public class LevelBoundsHandler : MonoBehaviour, IReinitializable
    {
        private Entity _entity;
        private LevelBounds _levelBounds;
        private readonly EntityPositionWrapper _positionWrapper = new();
        
        private bool _hasEnteredLevel;

        private void Awake()
        {
            _entity = GetComponent<Entity>();
        }

        [Inject]
        public void Construct(LevelBounds levelBounds)
        {
            _levelBounds = levelBounds;
        }
        

        private void FixedUpdate()
        {
            if (_levelBounds == null)
                return;
            
            Vector2 position = transform.position;
            
            if (!_hasEnteredLevel && _levelBounds.IsInsideLevel(position))
                _hasEnteredLevel = true;

            if (!_hasEnteredLevel)
                return;

            if (_levelBounds.IsOutsideOfBounds(position))
                _positionWrapper.WrapEntityPosition(_entity, _levelBounds);
        }

        public void Reinitialize() 
            => _hasEnteredLevel = false;
    }
}