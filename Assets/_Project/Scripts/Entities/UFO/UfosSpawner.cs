using System;
using _Project.Scripts.Entities.Asteroids;
using _Project.Scripts.Entities.Spawner;
using _Project.Scripts.Player;
using _Project.Scripts.Services.Pause;
using _Project.Scripts.Services.RemoteConfigs;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Entities.UFO
{
    public class UfosSpawner : MonoBehaviour, IPausable, IConfigFetcher
    {
        public event Action<Ufo> UfoSpawned;

        [SerializeField] private bool _drawGizmos;
        [SerializeField] private Color _gizmosColor;

        private RectangleSideSpawnPositionPicker _spawnPositionPicker;

        private SpawnerTimingConfig _timings;
        private GameObject _container;
        private UfoFactory _ufoFactory;
        private EnemyTarget _target;
        private bool _isActive;
        private float _timer;

        [Inject]
        private void Construct(UfoFactory ufoFactory)
        {
            _ufoFactory = ufoFactory;
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

        public void FetchConfig(IConfigsProvider configsProvider)
        {
            SimpleSpawnerConfig spawnerConfig = configsProvider.GetValue<SimpleSpawnerConfig>(ConfigsNames.UfoSpawner);
            _timings = spawnerConfig.Timings;
            _spawnPositionPicker = new RectangleSideSpawnPositionPicker(spawnerConfig.SpawnPositionSize, _gizmosColor);
        }

        private void SpawnUfo()
        {
            Ufo ufo = _ufoFactory.Create(_spawnPositionPicker.GetNextPosition(), _target);
            UfoSpawned?.Invoke(ufo);
        }

        private float GetNextTimer()
            => Random.Range(_timings.MinInterval, _timings.MaxInterval);

        public void SetTarget(EnemyTarget enemyTarget)
            => _target = enemyTarget;

        public void Pause()
            => _isActive = false;

        public void Resume()
            => _isActive = true;

        public void StartSpawning()
        {
            _isActive = true;
            _timer = _timings.StartDelay;
        }

        private void OnDrawGizmos()
        {
            if (!_drawGizmos)
                return;
            _spawnPositionPicker?.DrawGizmos();
        }
    }
}