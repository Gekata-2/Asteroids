using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace _Project.Scripts.Entities.Asteroids.Pools
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Create Asteroid Pools Config", fileName = "Asteroid Pools Config")]
    public class AsteroidPoolsConfig : ScriptableObject
    {
        [field: SerializeField]
        public SerializedDictionary<AsteroidType, AsteroidPoolConfig> PoolConfigs { get; private set; }
    }
}