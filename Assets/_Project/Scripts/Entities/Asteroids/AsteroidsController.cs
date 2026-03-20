using System;
using System.Collections.Generic;
using _Project.Scripts.Entities.Asteroids.Configs;
using _Project.Scripts.Level.BoundsHandling;
using ModestTree;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Entities.Asteroids
{
    public class AsteroidsController : MonoBehaviour
    {
        public event Action<Asteroid> AsteroidDestroyed;

        private AsteroidsSpawner _asteroidsSpawner;
        private EntitiesContainer _entitiesContainer;

        private readonly List<Asteroid> _asteroids = new();

        private EntityOutOfBoundsController _entityOutOfBoundsController;

        [Inject]
        private void Construct(AsteroidsSpawner asteroidsSpawner, EntitiesContainer entitiesContainer,
            EntityOutOfBoundsController entityOutOfBoundsController)
        {
            _asteroidsSpawner = asteroidsSpawner;
            _entitiesContainer = entitiesContainer;
            _entityOutOfBoundsController = entityOutOfBoundsController;
        }

        private void Start()
        {
            _asteroidsSpawner.AsteroidSpawned += OnAsteroidSpawned;
            _asteroidsSpawner.StartSpawning();
            _entityOutOfBoundsController.EntityOutOfOuterBoundsDestroyed += OnEntityOutOfOuterBoundsDestroyed;
        }

        private void OnDestroy()
        {
            _asteroidsSpawner.AsteroidSpawned -= OnAsteroidSpawned;

            foreach (Asteroid asteroid in _asteroids)
                UnsubscribeFromAsteroid(asteroid);

            _asteroids.Clear();
        }

        private void OnEntityOutOfOuterBoundsDestroyed(Entity entity)
        {
            if (entity is Asteroid asteroid)
            {
                UnsubscribeFromAsteroid(asteroid);
                _asteroids.Remove(asteroid);
            }
        }

        private void OnAsteroidSpawned(Asteroid asteroid)
        {
            _asteroids.Add(asteroid);
            _entitiesContainer.AddEntity(asteroid);
            SubscribeToAsteroid(asteroid);
        }

        private void SubscribeToAsteroid(Asteroid asteroid)
        {
            asteroid.CollidedWithBullet += OnAsteroidCollidedWithBullet;
            asteroid.SweepedByLaser += OnAsteroidsSeepedByLaser;
        }

        private void UnsubscribeFromAsteroid(Asteroid asteroid)
        {
            asteroid.CollidedWithBullet -= OnAsteroidCollidedWithBullet;
            asteroid.SweepedByLaser -= OnAsteroidsSeepedByLaser;
        }

        private void OnAsteroidCollidedWithBullet(Asteroid asteroid)
        {
            UnsubscribeFromAsteroid(asteroid);

            Queue<AsteroidsSplitConfig> chain = asteroid.SplitChain;
            DestroyAsteroid(asteroid);

            if (chain.IsEmpty())
                return;

            _asteroidsSpawner.SpawnAsteroidsFromPosition(chain, asteroid.transform.position);
            chain.Dequeue();
        }

        private void OnAsteroidsSeepedByLaser(Asteroid asteroid)
        {
            DestroyAsteroid(asteroid);
        }

        private void DestroyAsteroid(Asteroid asteroid)
        {
            asteroid.Die();

            _asteroids.Remove(asteroid);
            _entitiesContainer.RemoveEntity(asteroid);
            AsteroidDestroyed?.Invoke(asteroid);
        }
    }
}