using Player;
using Services.EventBus;

namespace Services.Awards
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