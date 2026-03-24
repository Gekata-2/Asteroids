using _Project.Scripts.Entities;
using _Project.Scripts.Level.GameSession;

namespace _Project.Scripts.Services.Awards
{
    public class EntityDestroyedAwardGiver : IAwardGiver<Entity>
    {
        private readonly GameSessionModel _playerModel;

        public EntityDestroyedAwardGiver(GameSessionModel playerModel)
        {
            _playerModel = playerModel;
        }

        public void GiveAwardFor(Entity entity)
            => _playerModel.AddScore(entity.Score);
    }
}