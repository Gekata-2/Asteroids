using System.Collections.Generic;
using _Project.Scripts.Level.BoundsHandling;
using _Project.Scripts.ObjectPools;
using _Project.Scripts.Services.AssetsManagement;
using _Project.Scripts.Services.BeginGame;
using _Project.Scripts.Sfx;
using UnityEngine;

namespace _Project.Scripts.Entities.Asteroids.Pools
{
    public class AsteroidPools : IAssetFetcher, IGameStarter
    {
        private const string CONTAINER_NAME = "Asteroids Container";

        private readonly Dictionary<AsteroidType, AsteroidPool> _pools = new();

        public AsteroidPools(PoolsConfigs configs, AsteroidPoolFactory poolFactory)
        {
            GameObject container = new GameObject(CONTAINER_NAME);
            foreach (var pair in configs.AsteroidsPools)
            {
                AssetPoolConfig config = pair.Value;
                AsteroidType asteroidType = pair.Key;
                AsteroidPool pool = poolFactory.Create(
                    new AsteroidPoolData(asteroidType, config.Asset, container.transform,
                        configs.InactiveObjectPosition, config.DefaultCapacity, config.MaxSize));
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