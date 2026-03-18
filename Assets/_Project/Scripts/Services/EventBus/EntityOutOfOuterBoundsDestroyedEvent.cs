using _Project.Scripts.Entities;

namespace _Project.Scripts.Services.EventBus
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