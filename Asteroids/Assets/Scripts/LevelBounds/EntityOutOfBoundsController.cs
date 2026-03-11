using System;
using System.Collections.Generic;
using Entities;
using Services.EventBus;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace LevelBounds
{
    class EntityOutOfBoundsController : IInitializable, IDisposable
    {
        private readonly LevelBounds _levelBounds;
        private readonly IPositionWrapper _positionWrapper;

        private EventBus _eventBus;

        public EntityOutOfBoundsController(IPositionWrapper positionWrapper, LevelBounds levelBounds, EventBus eventBus)
        {
            _positionWrapper = positionWrapper;
            _levelBounds = levelBounds;
            _eventBus = eventBus;
        }

        public void Initialize()
        {
            _levelBounds.EntitiesOutOfBounds += LevelBounds_OnEntitiesOutOfBounds;
            _levelBounds.EntitiesOutOfOuterBounds += LevelBounds_OnEntitiesOutOfOuterBounds;
        }

        private void LevelBounds_OnEntitiesOutOfOuterBounds(List<Entity> entities)
        {
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