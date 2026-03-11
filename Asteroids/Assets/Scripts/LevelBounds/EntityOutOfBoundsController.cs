using System;
using System.Collections.Generic;
using Entities;
using Services.EventBus;
using Zenject;
using Object = UnityEngine.Object;

namespace LevelBounds
{
    class EntityOutOfOuterBoundsDestroyed
    {
        public readonly Entity Entity;

        public EntityOutOfOuterBoundsDestroyed(Entity entity)
        {
            Entity = entity;
        }
    }

    class EntityOutOfBoundsController : IInitializable, IDisposable
    {
        private readonly LevelBounds _levelBounds;
        private readonly EntitiesContainer _entitiesContainer;
        private readonly IPositionWrapper _positionWrapper;

        private EventBus _eventBus;

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
            _levelBounds.EntitiesOutOfBounds += LevelBounds_OnEntitiesOutOfBounds;
            _levelBounds.EntitiesOutOfOuterBounds += LevelBounds_OnEntitiesOutOfOuterBounds;
        }

        private void LevelBounds_OnEntitiesOutOfOuterBounds(List<Entity> entities)
        {
            foreach (Entity entity in entities)
            {
                Object.Destroy(entity.gameObject);
                _entitiesContainer.RemoveEntity(entity);
                _eventBus.Invoke(new EntityOutOfOuterBoundsDestroyed(entity));
            }
        }

        private void LevelBounds_OnEntitiesOutOfBounds(List<Entity> entities)
        {
            foreach (Entity entity in entities)
                _positionWrapper.WrapEntityPosition(entity, _levelBounds);
        }

        public void Dispose()
        {
            _levelBounds.EntitiesOutOfBounds -= LevelBounds_OnEntitiesOutOfBounds;
            _levelBounds.EntitiesOutOfOuterBounds -= LevelBounds_OnEntitiesOutOfOuterBounds;
        }
    }
}