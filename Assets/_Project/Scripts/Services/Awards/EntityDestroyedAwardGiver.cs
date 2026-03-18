using _Project.Scripts.Level.GameSession;
using _Project.Scripts.Services.EventBus;

namespace _Project.Scripts.Services.Awards
{
    public class EntityDestroyedAwardGiver : IAwardGiver<EntityDestroyedEvent>
    {
        private readonly GameSessionModel _playerModel;

        public EntityDestroyedAwardGiver(GameSessionModel playerModel)
        {
            _playerModel = playerModel;
        }

        public void GiveAwardFor(EntityDestroyedEvent entityDestroyedEvent) 
            => _playerModel.AddScore(entityDestroyedEvent.Entity.Data.Score);
    }
}