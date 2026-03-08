using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Asteroids
{
    public class AsteroidsSpawner : MonoBehaviour
    {
        public event Action<Asteroid> AsteroidSpawned;

        [SerializeField] private Transform asteroidsContainer;

        private AsteroidsConfig _asteroidsConfig;
        private AsteroidsSpawnerConfig _spawnerConfig;

        public Transform AsteroidsContainer => asteroidsContainer;

        [Inject]
        private void Construct(AsteroidsConfig asteroidsConfig, AsteroidsSpawnerConfig spawnerConfig)
        {
            _asteroidsConfig = asteroidsConfig;
            _spawnerConfig = spawnerConfig;
        }

        private float _nextSpawnTimestamp;

        private void Start()
        {
            _nextSpawnTimestamp = float.MaxValue;
        }

        private void Update()
        {
            if (Time.time >= _nextSpawnTimestamp)
            {
                SpawnAsteroid();
                _nextSpawnTimestamp = GetNextSpawnTimestamp();
            }
        }


        public void StartSpawning()
        {
            _nextSpawnTimestamp = Time.time + _spawnerConfig.StartDelay;
        }

        public void StopSpawning()
        {
        }

        private void SpawnAsteroid()
        {
            AsteroidData asteroidData = _asteroidsConfig.Chain.First().Data;
            Asteroid asteroid = Instantiate(asteroidData.Prefab, asteroidsContainer);

            Queue<AsteroidsChainData> splitChain = new(_asteroidsConfig.Chain);
            splitChain.Dequeue();

            asteroid.Initialize(GetAsteroidSpeed(asteroidData.Speed), GetAsteroidDirection(), splitChain);
            AsteroidSpawned?.Invoke(asteroid);
        }

        public void SpawnAsteroids(Queue<AsteroidsChainData> chain, Vector3 position)
        {
            AsteroidsChainData split = chain.Peek();
            int newAsteroidsCount = Random.Range(split.MinNewAsteroids, split.MaxNewAsteroids);
            AsteroidData data = split.Data;
            for (int i = 0; i < newAsteroidsCount; i++)
            {
                Asteroid asteroid = Instantiate(data.Prefab, position, Quaternion.identity);
                //  asteroid.SetPosition(position);
                asteroid.Initialize(GetAsteroidSpeed(data.Speed), GetAsteroidDirection(), chain);
                asteroid.transform.localScale = Vector3.one * split.Size;
                AsteroidSpawned?.Invoke(asteroid);
            }
        }

        private float GetAsteroidSpeed(AsteroidData.AsteroidSpeedData speedData) =>
            Random.Range(speedData.min, speedData.max);

        private Vector2 GetAsteroidDirection()
            => Random.insideUnitCircle.normalized;

        private float GetNextSpawnTimestamp() =>
            Time.time + Random.Range(_spawnerConfig.MinInterval, _spawnerConfig.MaxInterval);
    }
}