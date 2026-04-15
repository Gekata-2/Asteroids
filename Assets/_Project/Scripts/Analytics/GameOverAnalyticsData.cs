using Newtonsoft.Json;

namespace _Project.Scripts.Analytics
{
    public readonly struct GameOverAnalyticsData
    {
        public int ShotsFired { get; }
        public int LaserUsed { get; }
        public int AsteroidsDestroyed { get; }
        public int UfoDestroyed { get; }

        public GameOverAnalyticsData(int shotsFired, int laserUsed, int asteroidsDestroyed, int ufoDestroyed)
        {
            ShotsFired = shotsFired;
            LaserUsed = laserUsed;
            AsteroidsDestroyed = asteroidsDestroyed;
            UfoDestroyed = ufoDestroyed;
        }

        public override string ToString() 
            => JsonConvert.SerializeObject(this);
    }
}