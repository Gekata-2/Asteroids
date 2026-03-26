using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Entities.Asteroids.Configs;
using _Project.Scripts.Level.BoundsHandling;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Entities.Asteroids
{
    public class AsteroidsController : MonoBehaviour
    {
        public event Action<Asteroid> AsteroidDestroyed;

        private EntitiesContainer _entitiesContainer;
        private AsteroidsSpawner _spawner;
        private AsteroidsConfig _asteroidsConfig;
        private LevelBounds _levelBounds;

        private readonly List<Asteroid> _asteroids = new();

        [Inject]
        private void Construct(EntitiesContainer entitiesContainer, AsteroidsSpawner spawner,
            AsteroidsConfig asteroidsConfig,
            LevelBounds levelBounds)
        {
            _entitiesContainer = entitiesContainer;
            _spawner = spawner;
            _asteroidsConfig = asteroidsConfig;
            _levelBounds = levelBounds;
        }

        private void Start()
        {
            _spawner.StartSpawning();
            _spawner.AsteroidSpawned += OnAsteroidSpawned;
        }

        private void OnDestroy()
        {
            _spawner.AsteroidSpawned -= OnAsteroidSpawned;

            foreach (Asteroid asteroid in _asteroids)
                UnsubscribeFromAsteroid(asteroid);

            _asteroids.Clear();
        }

        private void OnAsteroidSpawned(Asteroid asteroid)
        {
            AsteroidConfig asteroidConfig = _asteroidsConfig.Chain.First().Config;
            Queue<AsteroidsSplitConfig> splitChain = new(_asteroidsConfig.Chain);
            splitChain.Dequeue();
            asteroid.Initialize(
                new AsteroidsInitializationData(
                    GetAsteroidSpeed(asteroidConfig.Speed),
                    GetAsteroidInitialDirectionFromPosition(asteroid.transform.position),
                    GetAsteroidTorque(asteroidConfig.Torque),
                    splitChain,
                    asteroidConfig));

            HandleCreatedAsteroid(asteroid);
        }
        
        private float GetAsteroidSpeed(AsteroidSpeedConfig speedConfig) =>
            Random.Range(speedConfig.Min, speedConfig.Max);

        private Vector2 GetAsteroidInitialDirectionFromPosition(Vector2 spawnPosition)
        {
            Vector2 positionInsideLevelBounds = new(
                Random.Range(_levelBounds.Bounds.min.x, _levelBounds.Bounds.max.x),
                Random.Range(_levelBounds.Bounds.min.y, _levelBounds.Bounds.max.y));

            return (positionInsideLevelBounds - spawnPosition).normalized;
        }

        private float GetAsteroidTorque(AsteroidTorqueConfig torqueConfig)
            => Random.Range(torqueConfig.Min, torqueConfig.Max);

        private void HandleCreatedAsteroid(Asteroid asteroid)
        {
            if (asteroid.TryGetComponent(out LevelBoundsHandler levelBoundsHandler))
                levelBoundsHandler.Initialize(_levelBounds);

            _asteroids.Add(asteroid);
            _entitiesContainer.AddEntity(asteroid);
            
            asteroid.Destroyed += OnAsteroidDestroyed;
            asteroid.CreatedDebris += OnCreatedDebris;
        }

        private void OnAsteroidDestroyed(Asteroid asteroid)
        {
            _asteroids.Remove(asteroid);
            _entitiesContainer.RemoveEntity(asteroid);
            UnsubscribeFromAsteroid(asteroid);
            AsteroidDestroyed?.Invoke(asteroid);
        }

        private void UnsubscribeFromAsteroid(Asteroid asteroid)
        {
            asteroid.Destroyed -= OnAsteroidDestroyed;
            asteroid.CreatedDebris -= OnCreatedDebris;
        }

        private void OnCreatedDebris(List<Asteroid> asteroids)
        {
            foreach (Asteroid asteroid in asteroids)
                HandleCreatedAsteroid(asteroid);
        }
    }
}