using UnityEngine;

namespace _Project.Scripts.Entities.Asteroids.Pools
{
    public struct AsteroidPoolData
    {
        public AsteroidType AsteroidType { get; }
        public string AssetName { get; }
        public Transform Container { get; }
        public Vector2 DefaultInactivePosition { get; }
        public int DefaultCapacity { get; }
        public int MaxSize { get; }

        public AsteroidPoolData(AsteroidType asteroidType, 
            string assetName,
            Transform container,
            Vector2 defaultInactivePosition, 
            int defaultCapacity, int maxSize)
        {
            AsteroidType = asteroidType;
            Container = container;
            DefaultInactivePosition = defaultInactivePosition;
            DefaultCapacity = defaultCapacity;
            MaxSize = maxSize;
            AssetName = assetName;
        }
    }
}