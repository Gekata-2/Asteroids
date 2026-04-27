using _Project.Scripts.Entities.Asteroids.Pools;
using _Project.Scripts.Entities.UFO;

namespace _Project.Scripts.Entities.Asteroids.Configs
{
    public class AsteroidConfig : EntityConfig
    {
        public AsteroidType AsteroidType { get; set; }
        public AsteroidSpeedConfig Speed { get; set; }
        public AsteroidTorqueConfig Torque { get; set; }
    }
}