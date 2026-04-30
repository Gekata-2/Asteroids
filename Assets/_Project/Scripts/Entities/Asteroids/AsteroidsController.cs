using System.Collections.Generic;
using _Project.Scripts.Entities.Asteroids.Configs;
using _Project.Scripts.Entities.Asteroids.Pools;
using _Project.Scripts.Level.BoundsHandling;
using _Project.Scripts.Level.GameSession;
using _Project.Scripts.Services.BeginGame;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Entities.Asteroids
{
    public class AsteroidsController : EntitiesController, IGameStarter
    {
        private EntitiesContainer _entitiesContainer;
        private AsteroidsSpawner _spawner;
        private List<AsteroidsSplitConfig> _asteroidSplitChain;
        private LevelBounds _levelBounds;
        private AsteroidPools _pools;
        private AsteroidsConfigsRegistry _asteroidsConfigsRegistry;
        private GameSessionData _sessionData;

        private readonly List<Asteroid> _asteroids = new();

        [Inject]
        private void Construct(EntitiesContainer entitiesContainer, AsteroidsSpawner spawner,
            LevelBounds levelBounds, AsteroidPools pools,
            AsteroidsConfigsRegistry asteroidsConfigsRegistry, GameSessionData sessionData)
        {
            _entitiesContainer = entitiesContainer;
            _spawner = spawner;
            _levelBounds = levelBounds;
            _pools = pools;
            _asteroidsConfigsRegistry = asteroidsConfigsRegistry;
            _sessionData = sessionData;
        }

        private void Start()
        {
            _spawner.AsteroidSpawned += OnAsteroidSpawned;
        }


        private void OnDestroy()
        {
            _spawner.AsteroidSpawned -= OnAsteroidSpawned;

            foreach (Asteroid asteroid in _asteroids)
                UnsubscribeFromAsteroid(asteroid);

            _asteroids.Clear();
        }

        public void BeginGame()
        {
            _spawner.StartSpawning();
        }

        private void OnAsteroidSpawned(Asteroid asteroid, Vector2 spawnPosition)
        {
            AsteroidConfig asteroidConfig = _asteroidsConfigsRegistry.GeFirstConfig();
            Queue<AsteroidsSplitConfig> splitChain = new(_asteroidsConfigsRegistry.Chain);
            splitChain.Dequeue();
            asteroid.Initialize(
                new AsteroidsInitializationData(
                    GetAsteroidSpeed(asteroidConfig.Speed),
                    GetAsteroidInitialDirectionFromPosition(spawnPosition),
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
                Random.Range(_levelBounds.Bounds.min.x + _levelBounds.SkinWidth,
                    _levelBounds.Bounds.max.x - _levelBounds.SkinWidth),
                Random.Range(_levelBounds.Bounds.min.y + _levelBounds.SkinWidth,
                    _levelBounds.Bounds.max.y - _levelBounds.SkinWidth));

            return (positionInsideLevelBounds - spawnPosition).normalized;
        }

        private float GetAsteroidTorque(AsteroidTorqueConfig torqueConfig)
            => Random.Range(torqueConfig.Min, torqueConfig.Max);

        private void HandleCreatedAsteroid(Asteroid asteroid)
        {
            asteroid.Destroyed += OnAsteroidDestroyed;
            asteroid.CreatedDebris += OnCreatedDebris;

            _asteroids.Add(asteroid);
            _entitiesContainer.AddEntity(asteroid);
        }

        private void OnAsteroidDestroyed(Asteroid asteroid)
        {
            _pools.Release(asteroid);
            _asteroids.Remove(asteroid);
            _entitiesContainer.RemoveEntity(asteroid);

            _sessionData.AddAsteroidDestroyed();

            UnsubscribeFromAsteroid(asteroid);

            NotifyAboutEntityDestroyed(asteroid);
        }

        private void OnCreatedDebris(List<Asteroid> asteroids)
        {
            foreach (Asteroid asteroid in asteroids)
                HandleCreatedAsteroid(asteroid);
        }

        private void UnsubscribeFromAsteroid(Asteroid asteroid)
        {
            asteroid.Destroyed -= OnAsteroidDestroyed;
            asteroid.CreatedDebris -= OnCreatedDebris;
        }
    }
}