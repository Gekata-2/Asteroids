using System;
using System.Collections.Generic;
using _Project.Scripts.Entities;
using _Project.Scripts.Services.EventBus;
using Zenject;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Level.BoundsHandling
{
    class EntityOutOfBoundsController : IInitializable, IDisposable
    {
        private readonly LevelBounds _levelBounds;
        private readonly EntitiesContainer _entitiesContainer;
        private readonly IPositionWrapper _positionWrapper;

        private readonly EventBus _eventBus;

        public EntityOutOfBoundsController(IPositionWrapper positionWrapper, LevelBounds levelBounds, EventBus eventBus,
            EntitiesContainer entitiesContainer)
        {
            _positionWrapper = positionWrapper;
            _levelBounds = levelBounds;
            _eventBus = eventBus;
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
                _eventBus.Invoke(new EntityOutOfOuterBoundsDestroyedEvent(entity));
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