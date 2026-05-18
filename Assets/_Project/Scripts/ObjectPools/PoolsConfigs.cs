using _Project.Scripts.Entities.Asteroids.Pools;
using _Project.Scripts.Vfx;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace _Project.Scripts.ObjectPools
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Create Pools Configs", fileName = "Pools Configs", order = 0)]
    public class PoolsConfigs : ScriptableObject
    {
        [field: SerializeField] public Vector2 InactiveObjectPosition { get; private set; } = new(50, 50);
        [field: Space] [field: SerializeField] public PoolConfig Sfx { get; private set; }

        [field: Space]
        [field: SerializeField]
        public SerializedDictionary<AsteroidType, AssetPoolConfig> AsteroidsPools { get; private set; }

        [field: SerializeField] public SerializedDictionary<VFX, PoolConfig> VfxPools { get; private set; }
    }
}