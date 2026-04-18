using System.Collections.Generic;
using _Project.Scripts.Level.BoundsHandling;
using _Project.Scripts.Services.BeginGame;
using UnityEngine;

namespace _Project.Scripts.Entities.Asteroids.Pools
{
    public class AsteroidPools : IAssetFetcher, IGameStarter
    {
        private readonly Dictionary<AsteroidType, AsteroidPool> _pools = new();
        private readonly GameObject _container;

        public AsteroidPools(AsteroidPoolsConfig configs, LevelBounds levelBounds, AsteroidPoolFactory poolFactory)
        {
            GameObject container = new GameObject("Asteroids Container");
            Vector2 defaultPosition = new Vector2(levelBounds.Bounds.max.x * 2, levelBounds.Bounds.max.y * 2);
            foreach (var pair in configs.PoolConfigs)
            {
                AsteroidPoolConfig config = pair.Value;
                AsteroidType asteroidType = pair.Key;
                AsteroidPool pool = poolFactory.Create(
                    new AsteroidPoolData(asteroidType, config.AssetName, container.transform,
                        defaultPosition, config.DefaultCapacity, config.MaxSize));
                _pools.Add(asteroidType, pool);
            }
        }

        public void FetchAssets()
        {
            foreach (AsteroidPool pool in _pools.Values)
                pool.FetchPrefab();
        }

        public void BeginGame()
        {
            foreach (AsteroidPool pool in _pools.Values)
                pool.PreWarm();
        }

        public Asteroid Get(AsteroidType type)
            => _pools[type].Get();

        public void Release(Asteroid asteroid)
            => _pools[asteroid.Type].Release(asteroid);
    }
}