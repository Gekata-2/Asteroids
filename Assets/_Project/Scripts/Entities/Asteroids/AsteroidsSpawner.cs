using System;
using System.Linq;
using _Project.Scripts.Entities.Asteroids.Configs;
using _Project.Scripts.Entities.Asteroids.Pools;
using _Project.Scripts.Entities.Factories;
using _Project.Scripts.Entities.Spawner;
using _Project.Scripts.Services.Pause;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Entities.Asteroids
{
    public class AsteroidsSpawner : MonoBehaviour, IPausable
    {
        public event Action<Asteroid, Vector2> AsteroidSpawned;

        [SerializeField] private bool drawGizmos;

        private SimpleSpawnerConfig _spawnerConfig;
        private RectangleSideSpawnPositionPicker _spawnPositionPicker;

        private AsteroidType _type;

        private float _timer;
        private bool _isActive;
        private AsteroidPools _pools;

        [Inject]
        private void Construct(AsteroidsConfig asteroidsConfig, SimpleSpawnerConfig spawnerConfig,
            RectangleSideSpawnPositionPicker spawnPositionPicker, AsteroidPools pools)
        {
            _pools = pools;
            _spawnerConfig = spawnerConfig;
            _spawnPositionPicker = spawnPositionPicker;

            _type = asteroidsConfig.Chain.First().Config.AsteroidType;
        }

        private void Start()
        {
            _timer = float.MaxValue;
        }

        private void Update()
        {
            if (!_isActive)
                return;

            _timer -= Time.deltaTime;

            if (_timer <= 0f)
            {
                SpawnAsteroid();
                _timer = GetNextTimer();
            }
        }

        private void SpawnAsteroid()
        {
            Vector2 spawnPosition = _spawnPositionPicker.GetNextPosition();
            Asteroid asteroid = _pools.Get(_type);
            
            asteroid.SetPosition(spawnPosition);
            
            AsteroidSpawned?.Invoke(asteroid,spawnPosition);
        }

        private float GetNextTimer()
            => Random.Range(_spawnerConfig.MinInterval, _spawnerConfig.MaxInterval);

        public void Pause()
            => _isActive = false;

        public void Resume()
            => _isActive = true;

        public void StartSpawning()
        {
            _isActive = true;
            _timer = _spawnerConfig.StartDelay;
        }

        private void OnDrawGizmos()
        {
            if (drawGizmos)
                _spawnPositionPicker?.DrawGizmos();
        }
    }
}