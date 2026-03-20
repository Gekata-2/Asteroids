using UnityEngine;

namespace _Project.Scripts.Entities.Asteroids.Configs
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Create Asteroid Data", fileName = "Asteroid Data", order = 0)]
    public class AsteroidConfig : EntityConfig
    {
        [field: SerializeField] public Asteroid Prefab { get; private set; }
        [field: SerializeField] public AsteroidSpeedConfig Speed { get; private set; }
        [field: SerializeField] public AsteroidTorqueConfig Torque { get; private set; }
    }
}