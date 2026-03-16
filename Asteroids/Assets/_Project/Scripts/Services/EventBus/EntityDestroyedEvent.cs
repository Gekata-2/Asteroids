using _Project.Scripts.Entities;

namespace _Project.Scripts.Services.EventBus
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