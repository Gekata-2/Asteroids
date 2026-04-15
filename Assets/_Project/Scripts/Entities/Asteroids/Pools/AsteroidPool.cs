using _Project.Scripts.Extensions;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Scripts.Entities.Asteroids.Pools
{
    public class AsteroidPool
    {
        private readonly ObjectPool<Asteroid> _pool;
        private readonly AsteroidFactory _factory;
        private readonly AsteroidPoolData _data;
        private readonly int _prewarmSize;

        public AsteroidPool(AsteroidFactory factory, AsteroidPoolData data)
        {
            _factory = factory;
            _data = data;
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

        public void Release(Asteroid asteroid)
            => _pool.Release(asteroid);

        public Asteroid Get()
            => _pool.Get();

        private Asteroid CreateAsteroid()
        {
            Asteroid asteroid = _factory.Create(_data.Prefab);
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
            Debug.Log("ZXC");
            Object.Destroy(asteroid.gameObject);
        }
    }
}