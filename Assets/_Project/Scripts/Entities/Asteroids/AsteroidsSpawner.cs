using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Entities.Asteroids.Configs;
using _Project.Scripts.Entities.Spawner;
using _Project.Scripts.Level.BoundsHandling;
using _Project.Scripts.Services;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Entities.Asteroids
{
    public class AsteroidsSpawner : MonoBehaviour, IPausable
    {
        public event Action<Asteroid> AsteroidSpawned;

        [SerializeField] private Transform asteroidsContainer;
        [SerializeField] private bool drawGizmos;

        private AsteroidsConfig _asteroidsConfig;
        private SimpleSpawnerConfig _spawnerConfig;
        private LevelBounds _levelBounds;
        private ISpawnPositionPicker _spawnPositionPicker;

        private float _timer;
        private bool _isActive;

        [Inject]
        private void Construct(AsteroidsConfig asteroidsConfig, SimpleSpawnerConfig spawnerConfig,
            LevelBounds levelBounds, ISpawnPositionPicker spawnPositionPicker)
        {
            _asteroidsConfig = asteroidsConfig;
            _spawnerConfig = spawnerConfig;
            _levelBounds = levelBounds;
            _spawnPositionPicker = spawnPositionPicker;
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
            AsteroidConfig asteroidConfig = _asteroidsConfig.Chain.First().Config;
            Vector2 spawnPosition = _spawnPositionPicker.GetNextPosition();
            Asteroid asteroid = Instantiate(asteroidConfig.Prefab, spawnPosition, Quaternion.identity);

            Queue<AsteroidsSplitConfig> splitChain = new(_asteroidsConfig.Chain);
            splitChain.Dequeue();

            asteroid.transform.parent = asteroidsContainer;
            asteroid.Initialize(
                new AsteroidsInitializationData(
                    GetAsteroidSpeed(asteroidConfig.Speed),
                    GetAsteroidInitialDirection(spawnPosition),
                    GetAsteroidTorque(asteroidConfig.Torque),
                    splitChain,
                    asteroidConfig));
            
            AsteroidSpawned?.Invoke(asteroid);
        }

        private float GetNextTimer()
            => Random.Range(_spawnerConfig.MinInterval, _spawnerConfig.MaxInterval);

        public void SpawnAsteroidsFromPosition(Queue<AsteroidsSplitConfig> asteroidsChainRemainder, Vector3 position)
        {
            AsteroidsSplitConfig split = asteroidsChainRemainder.Peek();
            AsteroidConfig asteroidConfig = split.Config;

            int newAsteroidsCount = Random.Range(split.MinNewAsteroids, split.MaxNewAsteroids + 1);

            for (int i = 0; i < newAsteroidsCount; i++)
            {
                Asteroid asteroid = Instantiate(asteroidConfig.Prefab, position, Quaternion.identity);
                asteroid.Initialize(
                    new AsteroidsInitializationData(
                        GetAsteroidSpeed(asteroidConfig.Speed),
                        GetAsteroidRandomDirection(),
                        GetAsteroidTorque(asteroidConfig.Torque),
                        asteroidsChainRemainder,
                        asteroidConfig));
                asteroid.transform.localScale = Vector3.one * split.Size;
                AsteroidSpawned?.Invoke(asteroid);
            }
        }

        private float GetAsteroidSpeed(AsteroidSpeedConfig speedConfig) =>
            Random.Range(speedConfig.Min, speedConfig.Max);

        private Vector2 GetAsteroidRandomDirection()
            => Random.insideUnitCircle.normalized;

        private Vector2 GetAsteroidInitialDirection(Vector2 spawnPosition)
        {
            Vector2 positionInsideLevelBounds = new Vector2(
                Random.Range(_levelBounds.Bounds.min.x, _levelBounds.Bounds.max.x),
                Random.Range(_levelBounds.Bounds.min.y, _levelBounds.Bounds.max.y));

            return (positionInsideLevelBounds - spawnPosition).normalized;
        }

        private float GetAsteroidTorque(AsteroidTorqueConfig torqueConfig)
            => Random.Range(torqueConfig.Min, torqueConfig.Max);


        public void Pause()
        {
            _isActive = false;
        }

        public void Resume()
        {
            _isActive = true;
        }

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