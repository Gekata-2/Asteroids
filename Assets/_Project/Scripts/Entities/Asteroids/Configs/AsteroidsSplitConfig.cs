using _Project.Scripts.Entities.Asteroids.Pools;

namespace _Project.Scripts.Entities.Asteroids.Configs
{
    public class AsteroidsSplitConfig
    {
        public AsteroidType AsteroidType { get; set; }
        public int MinNewAsteroids { get; set; } = 1;
        public int MaxNewAsteroids { get; set; } = 1;
        public float Size { get; set; } = 1;
    }
}