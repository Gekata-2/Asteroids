using _Project.Scripts.Extensions;
using _Project.Scripts.Services.AssetsProviding;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Scripts.Entities.Asteroids.Pools
{
    public class AsteroidPool
    {
        private readonly ObjectPool<Asteroid> _pool;
        private readonly AsteroidFactory _factory;
        private readonly AssetsNames _assetsNames;
        private readonly IAssetProvider _assetProvider;
        private readonly AsteroidPoolData _data;
        private readonly int _prewarmSize;

        private Object _prefab;

        public AsteroidPool(AsteroidFactory factory,
            AsteroidPoolData data,
            AssetsNames assetsNames, IAssetProvider assetProvider)
        {
            _factory = factory;
            _data = data;
            _assetProvider = assetProvider;
            _assetsNames = assetsNames;
            _pool = new ObjectPool<Asteroid>(
                CreateAsteroid,
                OnTakeAsteroidFromPool,
                OnReturnAsteroidToPool,
                OnDestroyAsteroid,
                collectionCheck: true,
                defaultCapacity: data.DefaultCapacity,
                maxSize: data.MaxSize);
        }

        public void PreWarm()
            => _pool.PreWarm(_data.DefaultCapacity);

        public void FetchPrefab()
            => _assetProvider.TryGetAsset(_assetsNames.GetName(_data.Asset), out _prefab);

        public void Release(Asteroid asteroid)
            => _pool.Release(asteroid);

        public Asteroid Get()
            => _pool.Get();

        private Asteroid CreateAsteroid()
        {
            if (_prefab == null)
                FetchPrefab();

            Asteroid asteroid = _factory.Create(_prefab);
            asteroid.transform.parent = _data.Container;
            asteroid.SetPositionImmediate(_data.DefaultInactivePosition);
            asteroid.SetType(_data.AsteroidType);
            return asteroid;
        }

        private void OnTakeAsteroidFromPool(Asteroid asteroid)
        {
            asteroid.gameObject.SetActive(true);
        }

        private void OnReturnAsteroidToPool(Asteroid asteroid)
        {
            asteroid.SetPositionImmediate(_data.DefaultInactivePosition);
            foreach (IReinitializable reinitializable in asteroid.gameObject.GetComponents<IReinitializable>())
                reinitializable.Reinitialize();
            asteroid.gameObject.SetActive(false);
        }

        private void OnDestroyAsteroid(Asteroid asteroid)
        {
            Object.Destroy(asteroid.gameObject);
        }
    }
}