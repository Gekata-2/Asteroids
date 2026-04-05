using _Project.Scripts.Entities.Asteroids.Pools;
using UnityEngine;

namespace _Project.Scripts.Entities.Asteroids.Configs
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Create Asteroid Config", fileName = "Asteroid Config", order = 0)]
    public class AsteroidConfig : EntityConfig
    {
        [field: SerializeField] public AsteroidType AsteroidType { get; private set; }
        [field: SerializeField] public AsteroidSpeedConfig Speed { get; private set; }
        [field: SerializeField] public AsteroidTorqueConfig Torque { get; private set; }
    }
}