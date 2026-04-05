using System;
using _Project.Scripts.Entities.Spawner;
using _Project.Scripts.Entities.UFO.Configs;
using _Project.Scripts.Player;
using _Project.Scripts.Services.Pause;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Entities.UFO
{
    public class UfosSpawner : MonoBehaviour, IPausable
    {
        public event Action<Ufo> UfoSpawned;

        [SerializeField] private bool drawGizmos;

        private UfoConfig _ufoConfig;
        private SimpleSpawnerConfig _spawnerConfig;

        private RectangleSideSpawnPositionPicker _spawnPositionPicker;

        private float _timer;
        private bool _isActive;
        private GameObject _container;
        private UfoFactory _ufoFactory;
        private EnemyTarget _target;

        [Inject]
        private void Construct(SimpleSpawnerConfig spawnerConfig, UfoFactory ufoFactory,
            RectangleSideSpawnPositionPicker spawnPositionPicker)
        {
            _spawnerConfig = spawnerConfig;
            _ufoFactory = ufoFactory;
            _spawnPositionPicker = spawnPositionPicker;
        }

        private void Start()
        {
            _timer = float.MaxValue;
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
            Ufo ufo = _ufoFactory.Create(_spawnPositionPicker.GetNextPosition(), _target);
            UfoSpawned?.Invoke(ufo);
        }

        private float GetNextTimer()
            => Random.Range(_spawnerConfig.MinInterval, _spawnerConfig.MaxInterval);

        public void SetTarget(EnemyTarget enemyTarget) => _target = enemyTarget;

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