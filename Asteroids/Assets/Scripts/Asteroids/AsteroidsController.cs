using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class AsteroidsController : MonoBehaviour
    {
        private AsteroidsSpawner _asteroidsSpawner;
        private List<Asteroid> _asteroids;

        [Inject]
        private void Construct(AsteroidsSpawner asteroidsSpawner)
        {
            _asteroidsSpawner = asteroidsSpawner;
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
        }
    }
}