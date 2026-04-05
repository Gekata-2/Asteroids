using UnityEngine;

namespace _Project.Scripts.Entities.Asteroids.Pools
{
    public struct AsteroidPoolData
    {
        public AsteroidType AsteroidType { get; }
        public Asteroid Prefab { get; }
        public Transform Container { get; }
        public Vector2 DefaultInactivePosition { get; }
        public int DefaultCapacity { get; }
        public int MaxSize { get; }

        public AsteroidPoolData(AsteroidType asteroidType, 
            Asteroid prefab,
            Transform container,
            Vector2 defaultInactivePosition,
            int defaultCapacity, 
            int maxSize)
        {
            AsteroidType = asteroidType;
            Prefab = prefab;
            Container = container;
            DefaultInactivePosition = defaultInactivePosition;
            DefaultCapacity = defaultCapacity;
            MaxSize = maxSize;
        }
    }
}