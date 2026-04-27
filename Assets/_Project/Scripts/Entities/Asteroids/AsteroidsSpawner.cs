using System;
using System.Linq;
using _Project.Scripts.Entities.Asteroids.Pools;
using _Project.Scripts.Entities.Spawner;
using _Project.Scripts.Services.Pause;
using _Project.Scripts.Services.RemoteConfigs;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Entities.Asteroids
{
    public class AsteroidsSpawner : MonoBehaviour, IPausable, IConfigFetcher
    {
        public event Action<Asteroid, Vector2> AsteroidSpawned;

        [SerializeField] private bool _drawGizmos;
        [SerializeField] private Color _gizmosColor;

        private RectangleSideSpawnPositionPicker _spawnPositionPicker;

        private AsteroidType _type;
        private float _timer;
        private bool _isActive;
        private AsteroidPools _pools;
        private SpawnerTimingConfig _timings;

        private AsteroidsConfigsRegistry _asteroidsConfigsRegistry;

        [Inject]
        private void Construct(AsteroidPools pools,
            AsteroidsConfigsRegistry asteroidsConfigsRegistry)
        {
            _pools = pools;
            _asteroidsConfigsRegistry = asteroidsConfigsRegistry;
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

        public void FetchConfig(IConfigsProvider configsProvider)
        {
            SimpleSpawnerConfig config = configsProvider.GetValue<SimpleSpawnerConfig>(ConfigsNames.AsteroidsSpawner);
            _timings = config.Timings;
            _spawnPositionPicker = new RectangleSideSpawnPositionPicker(config.SpawnPositionSize, _gizmosColor);

            _type = _asteroidsConfigsRegistry.Chain.First().AsteroidType;
        }

        private void SpawnAsteroid()
        {
            Vector2 spawnPosition = _spawnPositionPicker.GetNextPosition();
            Asteroid asteroid = _pools.Get(_type);

            asteroid.SetPositionImmediate(spawnPosition);

            AsteroidSpawned?.Invoke(asteroid, spawnPosition);
        }

        private float GetNextTimer()
            => Random.Range(_timings.MinInterval, _timings.MaxInterval);

        public void Pause()
            => _isActive = false;

        public void Resume()
            => _isActive = true;

        public void StartSpawning()
        {
            _isActive = true;
            _timer = _timings.StartDelay;
        }

        private void OnDrawGizmos()
        {
            if (_drawGizmos)
                _spawnPositionPicker?.DrawGizmos();
        }
    }
}