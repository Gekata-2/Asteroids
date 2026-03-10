using Entities;

namespace Services.EventBus
{
    public class EntityDestroyedEvent
    {
        public readonly Entity Entity;

        public EntityDestroyedEvent(Entity entity)
        {
            Entity = entity;
        }
    }
}