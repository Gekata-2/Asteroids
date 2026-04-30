using _Project.Scripts.Services.AssetsProviding;
using UnityEngine;

namespace _Project.Scripts.Entities.Asteroids.Pools
{
    public struct AsteroidPoolData
    {
        public AsteroidType AsteroidType { get; }
        public Asset Asset { get; }
        public Transform Container { get; }
        public Vector2 DefaultInactivePosition { get; }
        public int DefaultCapacity { get; }
        public int MaxSize { get; }

        public AsteroidPoolData(AsteroidType asteroidType, 
            Asset asset,
            Transform container,
            Vector2 defaultInactivePosition, 
            int defaultCapacity, int maxSize)
        {
            AsteroidType = asteroidType;
            Container = container;
            DefaultInactivePosition = defaultInactivePosition;
            DefaultCapacity = defaultCapacity;
            MaxSize = maxSize;
            Asset = asset;
        }
    }
}