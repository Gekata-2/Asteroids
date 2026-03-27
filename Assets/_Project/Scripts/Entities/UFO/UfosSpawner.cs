using System;
using _Project.Scripts.Entities.Spawner;
using _Project.Scripts.Entities.UFO.Configs;
using _Project.Scripts.Services.Pause;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Entities.UFO
{
    public class UfosSpawner : MonoBehaviour, IPausable
    {
        private const string CONTAINER_NAME = "UFO container";

        public event Action<Ufo> UfoSpawned;


        [SerializeField] private bool drawGizmos;

        private UfoConfig _ufoConfig;
        private SimpleSpawnerConfig _spawnerConfig;
        private Ufo _prefab;
        private RectangleSideSpawnPositionPicker _spawnPositionPicker;

        private float _timer;
        private bool _isActive;
        private GameObject _container;

        [Inject]
        private void Construct(UfoConfig ufoConfig, SimpleSpawnerConfig spawnerConfig,
            RectangleSideSpawnPositionPicker spawnPositionPicker)
        {
            _prefab = ufoConfig.Prefab;
            _spawnerConfig = spawnerConfig;
            _spawnPositionPicker = spawnPositionPicker;
        }

        private void Start()
        {
            _timer = float.MaxValue;
            _container = new GameObject(CONTAINER_NAME);
        }

        private void Update()
        {
            if (!_isActive)
                return;

            _timer -= Time.deltaTime;

            if (_timer <= 0f)
            {
                SpawnUfo();
                _timer = GetNextTimer();
            }
        }

        private void SpawnUfo()
        {
            Ufo ufo = Instantiate(_prefab, _spawnPositionPicker.GetNextPosition(), Quaternion.identity);
            ufo.transform.parent = _container.transform;

            UfoSpawned?.Invoke(ufo);
        }

        private float GetNextTimer()
            => Random.Range(_spawnerConfig.MinInterval, _spawnerConfig.MaxInterval);

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
            if (!drawGizmos)
                return;
            _spawnPositionPicker?.DrawGizmos();
        }
    }
}