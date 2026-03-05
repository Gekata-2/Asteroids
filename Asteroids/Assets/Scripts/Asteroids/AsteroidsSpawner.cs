using System;
using System.Linq;
using ModestTree;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Asteroids
{
    public class AsteroidsSpawner : MonoBehaviour
    {
        public event Action<Asteroid> AsteroidSpawned;

        [SerializeField] private Transform asteroidsContainer;

        private AsteroidsConfig _asteroidsConfig;
        private AsteroidsSpawnerConfig _spawnerConfig;

        [Inject]
        private void Construct(AsteroidsConfig asteroidsConfig, AsteroidsSpawnerConfig spawnerConfig)
        {
            _asteroidsConfig = asteroidsConfig;
            _spawnerConfig = spawnerConfig;
        }

        private float _nextSpawnTimestamp;

        private void Start()
        {
            _nextSpawnTimestamp = float.MaxValue;
        }

        private void Update()
        {
            if (_asteroidsConfig.AsteroidsData.IsEmpty())
            {
                Debug.LogWarning("Asteroids Data is Empty");
                return;
            }

            if (Time.time >= _nextSpawnTimestamp)
            {
                SpawnAsteroid();
                _nextSpawnTimestamp = GetNextSpawnTimestamp();
            }
        }


        public void StartSpawning()
        {
            _nextSpawnTimestamp = Time.time + _spawnerConfig.StartDelay;
        }

        public void StopSpawning()
        {
        }

        private void SpawnAsteroid()
        {
            AsteroidsConfig.AsteroidData asteroidData = _asteroidsConfig.AsteroidsData.First();
            Asteroid asteroid = Instantiate(asteroidData.Prefab, asteroidsContainer);
            asteroid.Initialize(GetAsteroidSpeed(asteroidData.Speed),GetAsteroidDirection());
            AsteroidSpawned?.Invoke(asteroid);
        }

        private float GetAsteroidSpeed(AsteroidsConfig.AsteroidSpeedData speedData) =>
            Random.Range(speedData.min, speedData.max);

        private Vector2 GetAsteroidDirection() 
            => Random.insideUnitCircle.normalized;

        private float GetNextSpawnTimestamp() =>
            Time.time + Random.Range(_spawnerConfig.MinInterval, _spawnerConfig.MaxInterval);
    }
}