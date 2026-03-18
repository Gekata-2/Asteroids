using _Project.Scripts.Player;
using _Project.Scripts.Services.EventBus;

namespace _Project.Scripts.Services.Awards
{
    public class EntityDestroyedAwardGiver : IAwardGiver<EntityDestroyedEvent>
    {
        private readonly PlayerModel _playerModel;

        public EntityDestroyedAwardGiver(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        public void GiveAwardFor(EntityDestroyedEvent entityDestroyedEvent) 
            => _playerModel.AddScore(entityDestroyedEvent.Entity.Data.Score);
    }
}