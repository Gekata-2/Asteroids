using System;
using Services;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Entities.UFO
{
    public class UfosSpawner : MonoBehaviour, IPausable
    {
        public event Action<UFO> UFOSpawned;

        [SerializeField] private Transform container;

        private UfoData _ufoData;
        private UfospawnerConfig _spawnerConfig;

        private float _timer;
        private bool _isActive;

        [Inject]
        private void Construct(UfoData ufoData, UfospawnerConfig spawnerConfig)
        {
            _ufoData = ufoData;
            _spawnerConfig = spawnerConfig;
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
                SpawnUFO();
                _timer = GetNextTimer();
            }
        }

        private void SpawnUFO()
        {
            UFO ufo = Instantiate(_ufoData.Prefab, container);
            UFOSpawned?.Invoke(ufo);
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
            _timer = GetNextTimer();
        }
    }
}