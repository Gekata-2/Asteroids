using System;
using System.Collections.Generic;
using _Project.Scripts.Entities;
using Zenject;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Level.BoundsHandling
{
    public class EntityOutOfBoundsController : IInitializable, IDisposable
    {
        public event Action<Entity> EntityOutOfOuterBoundsDestroyed;

        private readonly LevelBounds _levelBounds;
        private readonly EntitiesContainer _entitiesContainer;
        private readonly IPositionWrapper _positionWrapper;


        public EntityOutOfBoundsController(IPositionWrapper positionWrapper, LevelBounds levelBounds,
            EntitiesContainer entitiesContainer)
        {
            _positionWrapper = positionWrapper;
            _levelBounds = levelBounds;
            _entitiesContainer = entitiesContainer;
        }

        public void Initialize()
        {
            _levelBounds.EntitiesOutOfBounds += OnEntitiesOutOfBounds;
            _levelBounds.EntitiesOutOfOuterBounds += OnEntitiesOutOfOuterBounds;
            _levelBounds.EntitiesFirstTimeEnteredLevel += OnEntitiesFirstTimeEnteredLevel;
        }

        private void OnEntitiesOutOfOuterBounds(List<Entity> entities)
        {
            foreach (Entity entity in entities)
            {
                Object.Destroy(entity.gameObject);
                _entitiesContainer.RemoveEntity(entity);
                EntityOutOfOuterBoundsDestroyed?.Invoke(entity);
            }
        }

        private void OnEntitiesOutOfBounds(List<Entity> entities)
        {
            foreach (Entity entity in entities)
                _positionWrapper.WrapEntityPosition(entity, _levelBounds);
        }

        private void OnEntitiesFirstTimeEnteredLevel(List<Entity> entities)
        {
            foreach (Entity entity in entities)
                entity.MarkEnteredLevel();
        }

        public void Dispose()
        {
            _levelBounds.EntitiesOutOfBounds -= OnEntitiesOutOfBounds;
            _levelBounds.EntitiesOutOfOuterBounds -= OnEntitiesOutOfOuterBounds;
            _levelBounds.EntitiesFirstTimeEnteredLevel -= OnEntitiesFirstTimeEnteredLevel;
        }
    }
}