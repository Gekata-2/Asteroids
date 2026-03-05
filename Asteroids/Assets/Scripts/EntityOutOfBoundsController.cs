using System;
using System.Collections.Generic;
using Player;
using Zenject;

class EntityOutOfBoundsController : IInitializable, IDisposable
{
    private readonly LevelBounds _levelBounds;
    private readonly IPositionWrapper _positionWrapper;

    public EntityOutOfBoundsController(IPositionWrapper positionWrapper, LevelBounds levelBounds)
    {
        _positionWrapper = positionWrapper;
        _levelBounds = levelBounds;
    }

    public void Initialize()
    {
        _levelBounds.EntitiesOutOfBounds += LevelBounds_OnEntitiesOutOfBounds;
    }

    private void LevelBounds_OnEntitiesOutOfBounds(List<Entity> entities)
    {
        foreach (Entity entity in entities)
            _positionWrapper.WrapEntityPosition(entity, _levelBounds);
    }

    public void Dispose()
    {
        _levelBounds.EntitiesOutOfBounds -= LevelBounds_OnEntitiesOutOfBounds;
    }
}