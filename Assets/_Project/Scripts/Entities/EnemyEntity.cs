namespace _Project.Scripts.Entities
{
    public abstract class EnemyEntity : Entity
    {
        public int Score { get; private set; }

        protected void InitializeData(EntityConfig entityData)
        {
            Score = entityData.Score;
        }

        public abstract override void Pause();
        public abstract override void Resume();
    }
}