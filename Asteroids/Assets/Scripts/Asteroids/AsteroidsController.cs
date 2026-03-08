using System.Collections.Generic;
using Entities;
using ModestTree;
using Services;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class AsteroidsController : MonoBehaviour, IPausable
    {
        private AsteroidsSpawner _asteroidsSpawner;
        private List<Asteroid> _asteroids;
        private EntitiesContainer _entitiesContainer;

        [Inject]
        private void Construct(AsteroidsSpawner asteroidsSpawner, EntitiesContainer entitiesContainer)
        {
            _asteroidsSpawner = asteroidsSpawner;
            _entitiesContainer = entitiesContainer;
        }

        private void Awake()
        {
            _asteroids = new List<Asteroid>();
        }

        private void Start()
        {
            _asteroidsSpawner.AsteroidSpawned += AsteroidsSpawner_OnAsteroidSpawned;
            _asteroidsSpawner.StartSpawning();
        }

        private void OnDestroy()
        {
            _asteroidsSpawner.AsteroidSpawned -= AsteroidsSpawner_OnAsteroidSpawned;
        }

        private void AsteroidsSpawner_OnAsteroidSpawned(Asteroid asteroid)
        {
            _asteroids.Add(asteroid);
            _entitiesContainer.AddEntity(asteroid);
            asteroid.CollidedWithBullet += Asteroid_OnCollidedWithBullet;
        }

        private void Asteroid_OnCollidedWithBullet(Asteroid asteroid)
        {
            asteroid.CollidedWithBullet -= Asteroid_OnCollidedWithBullet;

            Queue<AsteroidsChainData> chain = asteroid.SplitChain;
            asteroid.Die();
            _asteroids.Remove(asteroid);
            _entitiesContainer.RemoveEntity(asteroid);
            
            if (chain.IsEmpty())
                return;
            _asteroidsSpawner.SpawnAsteroidsFromPosition(chain, asteroid.transform.position);
            chain.Dequeue();
        }

        public void Pause()
        {
            foreach (Asteroid asteroid in _asteroids) 
                asteroid.Pause();
        }

        public void Resume()
        {
            foreach (Asteroid asteroid in _asteroids) 
                asteroid.Resume();
        }
    }
}