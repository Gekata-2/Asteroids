using System.Collections.Generic;
using _Project.Scripts.Services.EventBus;
using ModestTree;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Entities.Asteroids
{
    public class AsteroidsController : MonoBehaviour
    {
        private AsteroidsSpawner _asteroidsSpawner;
        private EntitiesContainer _entitiesContainer;
        private EventBus _eventBus;
        
        private List<Asteroid> _asteroids;

        [Inject]
        private void Construct(AsteroidsSpawner asteroidsSpawner, EntitiesContainer entitiesContainer,
            EventBus eventBus)
        {
            _asteroidsSpawner = asteroidsSpawner;
            _entitiesContainer = entitiesContainer;
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _asteroids = new List<Asteroid>();
        }

        private void Start()
        {
            _asteroidsSpawner.AsteroidSpawned += AsteroidsSpawner_OnAsteroidSpawned;
            _eventBus.Subscribe<EntityOutOfOuterBoundsDestroyedEvent>(OnEntityOutOfOuterBoundsDestroyed);
            _asteroidsSpawner.StartSpawning();
        }

        private void OnDestroy()
        {
            _asteroidsSpawner.AsteroidSpawned -= AsteroidsSpawner_OnAsteroidSpawned;
            _eventBus.Unsubscribe<EntityOutOfOuterBoundsDestroyedEvent>(OnEntityOutOfOuterBoundsDestroyed);
            
            foreach (Asteroid asteroid in _asteroids)
                UnsubscribeFromAsteroid(asteroid);

            _asteroids.Clear();
        }

        private void OnEntityOutOfOuterBoundsDestroyed(EntityOutOfOuterBoundsDestroyedEvent @event)
        {
            if (@event.Entity is Asteroid asteroid)
            {
                UnsubscribeFromAsteroid(asteroid);
                _asteroids.Remove(asteroid);
            }
        }

        private void AsteroidsSpawner_OnAsteroidSpawned(Asteroid asteroid)
        {
            _asteroids.Add(asteroid);
            _entitiesContainer.AddEntity(asteroid);
            SubscribeToAsteroid(asteroid);
        }

        private void SubscribeToAsteroid(Asteroid asteroid)
        {
            asteroid.CollidedWithBullet += Asteroid_OnCollidedWithBullet;
            asteroid.SweepedByLaser += Asteroid_OnSweepedByLaser;
        }

        private void UnsubscribeFromAsteroid(Asteroid asteroid)
        {
            asteroid.CollidedWithBullet -= Asteroid_OnCollidedWithBullet;
            asteroid.SweepedByLaser -= Asteroid_OnSweepedByLaser;
        }

        private void Asteroid_OnCollidedWithBullet(Asteroid asteroid)
        {
            UnsubscribeFromAsteroid(asteroid);

            Queue<AsteroidsSplitData> chain = asteroid.SplitChain;
            DestroyAsteroid(asteroid);

            if (chain.IsEmpty())
                return;
            
            _asteroidsSpawner.SpawnAsteroidsFromPosition(chain, asteroid.transform.position);
            chain.Dequeue();
        }

        private void Asteroid_OnSweepedByLaser(Asteroid asteroid)
        {
            DestroyAsteroid(asteroid);
        }

        private void DestroyAsteroid(Asteroid asteroid)
        {
            asteroid.Die();
            
            _asteroids.Remove(asteroid);
            _entitiesContainer.RemoveEntity(asteroid);
            _eventBus.Invoke(new EntityDestroyedEvent(asteroid));
        }
    }
}