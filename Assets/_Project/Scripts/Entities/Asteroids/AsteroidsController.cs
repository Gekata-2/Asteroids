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
    public class AsteroidsController : MonoBehaviour, IPausable
    {
        public event Action<Asteroid> AsteroidDestroyed;

        [SerializeField] private Transform asteroidsContainer;
        [SerializeField] private bool drawGizmos;

        private EntitiesContainer _entitiesContainer;
        private EntityOutOfBoundsController _entityOutOfBoundsController;
        private AsteroidsConfig _asteroidsConfig;
        private SimpleSpawnerConfig _spawnerConfig;
        private LevelBounds _levelBounds;
        private ISpawnPositionPicker _spawnPositionPicker;

        private bool _isActive;
        private float _timer;
        private readonly List<Asteroid> _asteroids = new();

        [Inject]
        private void Construct(EntitiesContainer entitiesContainer,
            EntityOutOfBoundsController entityOutOfBoundsController, AsteroidsConfig asteroidsConfig,
            SimpleSpawnerConfig spawnerConfig,
            LevelBounds levelBounds, ISpawnPositionPicker spawnPositionPicker)
        {
            _entitiesContainer = entitiesContainer;
            _entityOutOfBoundsController = entityOutOfBoundsController;
            _asteroidsConfig = asteroidsConfig;
            _spawnerConfig = spawnerConfig;
            _levelBounds = levelBounds;
            _spawnPositionPicker = spawnPositionPicker;
        }

        private void Start()
        {
            _timer = float.MaxValue;
            _entityOutOfBoundsController.EntityOutOfOuterBoundsDestroyed += OnEntityOutOfOuterBoundsDestroyed;
            StartSpawning();
        }

        private void OnDestroy()
        {
            _entityOutOfBoundsController.EntityOutOfOuterBoundsDestroyed -= OnEntityOutOfOuterBoundsDestroyed;
            foreach (Asteroid asteroid in _asteroids)
                UnsubscribeFromAsteroid(asteroid);

            _asteroids.Clear();
        }

        private void Update()
        {
            if (!_isActive)
                return;

            _timer -= Time.deltaTime;

            if (_timer <= 0f)
            {
                _timer = GetNextTimer();
                SpawnAsteroid();
            }
        }

        private void StartSpawning()
        {
            _isActive = true;
            _timer = _spawnerConfig.StartDelay;
        }

        public void Pause()
        {
            _isActive = false;
        }

        public void Resume()
        {
            _isActive = true;
        }

        private float GetNextTimer()
            => Random.Range(_spawnerConfig.MinInterval, _spawnerConfig.MaxInterval);

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

            if (asteroid.TryGetComponent(out LevelBoundsHandler levelBoundsHandler))
            {
                levelBoundsHandler.Initialize(_levelBounds);
                levelBoundsHandler.Destroyed += OnDestroyedAfterCrossingOuterBounds;
            }

            HandleCreatedAsteroid(asteroid);
        }

        private float GetAsteroidSpeed(AsteroidSpeedConfig speedConfig) =>
            Random.Range(speedConfig.Min, speedConfig.Max);

        private Vector2 GetAsteroidInitialDirection(Vector2 spawnPosition)
        {
            Vector2 positionInsideLevelBounds = new(
                Random.Range(_levelBounds.Bounds.min.x, _levelBounds.Bounds.max.x),
                Random.Range(_levelBounds.Bounds.min.y, _levelBounds.Bounds.max.y));

            return (positionInsideLevelBounds - spawnPosition).normalized;
        }

        private float GetAsteroidTorque(AsteroidTorqueConfig torqueConfig)
            => Random.Range(torqueConfig.Min, torqueConfig.Max);

        private void SubscribeToAsteroid(Asteroid asteroid)
        {
            asteroid.Destroyed += OnAsteroidDestroyed;
            asteroid.CreatedDebris += OnCreatedDebris;
        }

        private void UnsubscribeFromAsteroid(Asteroid asteroid)
        {
            asteroid.Destroyed -= OnAsteroidDestroyed;
            asteroid.CreatedDebris -= OnCreatedDebris;
        }

        private void OnDestroyedAfterCrossingOuterBounds(Entity entity)
        {
        }

        private void OnAsteroidDestroyed(Asteroid asteroid)
        {
            HandleAsteroidDestroyed(asteroid);
        }

        private void OnCreatedDebris(List<Asteroid> asteroids)
        {
            foreach (Asteroid asteroid in asteroids)
                HandleCreatedAsteroid(asteroid);
        }

        private void HandleCreatedAsteroid(Asteroid asteroid)
        {
            _asteroids.Add(asteroid);
            _entitiesContainer.AddEntity(asteroid);
            SubscribeToAsteroid(asteroid);
        }

        private void HandleAsteroidDestroyed(Asteroid asteroid)
        {
            _asteroids.Remove(asteroid);
            _entitiesContainer.RemoveEntity(asteroid);
            UnsubscribeFromAsteroid(asteroid);
            AsteroidDestroyed?.Invoke(asteroid);
        }


        private void OnEntityOutOfOuterBoundsDestroyed(Entity entity)
        {
            if (entity is Asteroid asteroid)
            {
                UnsubscribeFromAsteroid(asteroid);
                _asteroids.Remove(asteroid);
            }
        }

        private void OnDrawGizmos()
        {
            if (drawGizmos)
                _spawnPositionPicker?.DrawGizmos();
        }
    }
}