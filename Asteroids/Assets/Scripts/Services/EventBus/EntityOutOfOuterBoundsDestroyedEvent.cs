using Entities;

namespace Services.EventBus
{
    class EntityOutOfOuterBoundsDestroyedEvent
    {
        public readonly Entity Entity;

        public EntityOutOfOuterBoundsDestroyedEvent(Entity entity)
        {
            Entity = entity;
        }
    }
}