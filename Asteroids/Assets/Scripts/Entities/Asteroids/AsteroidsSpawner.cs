using System;
using System.Collections.Generic;
using System.Linq;
using Entities.UFO;
using Services;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;


namespace Entities.Asteroids
{
    public class AsteroidsSpawner : MonoBehaviour, IPausable
    {
        public event Action<Asteroid> AsteroidSpawned;

        [SerializeField] private Transform asteroidsContainer;

        private AsteroidsConfig _asteroidsConfig;
        private SimpleSpawnerConfig _spawnerConfig;
        private RandomOuterSquarePositionPicker _spawnPositionPicker;
        private LevelBounds.LevelBounds _levelBounds;

        private float _timer;
        private bool _isActive;

        [Inject]
        private void Construct(AsteroidsConfig asteroidsConfig, SimpleSpawnerConfig spawnerConfig,
            LevelBounds.LevelBounds levelBounds)
        {
            _asteroidsConfig = asteroidsConfig;
            _spawnerConfig = spawnerConfig;
            _levelBounds = levelBounds;
        }

        private void Start()
        {
            _timer = float.MaxValue;
            _spawnPositionPicker = new RandomOuterSquarePositionPicker(_spawnerConfig.SpawnPositionSideLenght);
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
            AsteroidData asteroidData = _asteroidsConfig.Chain.First().Data;
            Vector2 spawnPosition = _spawnPositionPicker.GetNextPosition();
            Asteroid asteroid = Instantiate(asteroidData.Prefab, spawnPosition, Quaternion.identity);
            asteroid.transform.parent = asteroidsContainer;
            asteroid.InitializeData(asteroidData);

            Queue<AsteroidsChainData> splitChain = new(_asteroidsConfig.Chain);
            splitChain.Dequeue();

            asteroid.Initialize(GetAsteroidSpeed(asteroidData.Speed), GetAsteroidInitialDirection(spawnPosition),
                splitChain);
            asteroid.AddTorque(GetAsteroidTorque(asteroidData.Torque));
            AsteroidSpawned?.Invoke(asteroid);
        }

        private float GetNextTimer()
            => Random.Range(_spawnerConfig.MinInterval, _spawnerConfig.MaxInterval);

        public void SpawnAsteroidsFromPosition(Queue<AsteroidsChainData> asteroidsChainRemainder, Vector3 position)
        {
            AsteroidsChainData split = asteroidsChainRemainder.Peek();
            int newAsteroidsCount = Random.Range(split.MinNewAsteroids, split.MaxNewAsteroids);
            AsteroidData asteroidData = split.Data;
            for (int i = 0; i < newAsteroidsCount; i++)
            {
                Asteroid asteroid = Instantiate(asteroidData.Prefab, position, Quaternion.identity);
                asteroid.InitializeData(asteroidData);
                asteroid.Initialize(GetAsteroidSpeed(asteroidData.Speed), GetAsteroidRandomDirection(),
                    asteroidsChainRemainder);
                asteroid.transform.localScale = Vector3.one * split.Size;
                asteroid.AddTorque(GetAsteroidTorque(asteroidData.Torque));
                AsteroidSpawned?.Invoke(asteroid);
            }
        }

        private float GetAsteroidSpeed(AsteroidData.AsteroidSpeedData speedData) =>
            Random.Range(speedData.min, speedData.max);

        private Vector2 GetAsteroidRandomDirection()
            => Random.insideUnitCircle.normalized;

        private Vector2 GetAsteroidInitialDirection(Vector2 spawnPosition)
        {
            Vector2 positionInsideLevelBounds = new Vector2(
                Random.Range(_levelBounds.Bounds.min.x, _levelBounds.Bounds.max.x),
                Random.Range(_levelBounds.Bounds.min.y, _levelBounds.Bounds.max.y));

            return (positionInsideLevelBounds - spawnPosition).normalized;
        }

        private float GetAsteroidTorque(AsteroidData.AsteroidTorqueData torqueData)
            => Random.Range(torqueData.min, torqueData.max);


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
    }
}