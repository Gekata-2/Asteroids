using System;
using System.Linq;
using _Project.Scripts.Entities.Asteroids.Configs;
using _Project.Scripts.Entities.Spawner;
using _Project.Scripts.Services;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Entities.Asteroids
{
    public class AsteroidsSpawner : MonoBehaviour, IPausable
    {
        private const string CONTAINER_NAME = "Asteroids Container";

        public event Action<Asteroid> AsteroidSpawned;
  
        [SerializeField] private bool drawGizmos;
        
        private SimpleSpawnerConfig _spawnerConfig;
        private RectangleSideSpawnPositionPicker _spawnPositionPicker;
        private Asteroid _prefab;
        private GameObject _container;
        private float _timer;
        private bool _isActive;

        [Inject]
        private void Construct(AsteroidsConfig asteroidsConfig, SimpleSpawnerConfig spawnerConfig,
            RectangleSideSpawnPositionPicker spawnPositionPicker)
        {
            _spawnerConfig = spawnerConfig;
            _spawnPositionPicker = spawnPositionPicker;
            _prefab = asteroidsConfig.Chain.First().Config.Prefab;
        }

        private void Start()
        {
            _timer = float.MaxValue;
            _container = new GameObject(CONTAINER_NAME);
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
            Asteroid asteroid = Instantiate(_prefab, _spawnPositionPicker.GetNextPosition(), Quaternion.identity);
            asteroid.transform.parent = _container.transform;

            AsteroidSpawned?.Invoke(asteroid);
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