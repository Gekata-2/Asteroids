using System;
using _Project.Scripts.Entities.Spawner;
using _Project.Scripts.Services;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Entities.UFO
{
    public class UfosSpawner : MonoBehaviour, IPausable
    {
        public event Action<UFO> UFOSpawned;

        [SerializeField] private Transform container;
        [SerializeField] private bool drawGizmos;

        private UfoData _ufoData;
        private SimpleSpawnerConfig _spawnerConfig;

        private float _timer;
        private bool _isActive;
        private ISpawnPositionPicker _spawnPositionPicker;

        [Inject]
        private void Construct(UfoData ufoData, SimpleSpawnerConfig spawnerConfig,
            ISpawnPositionPicker spawnPositionPicker)
        {
            _ufoData = ufoData;
            _spawnerConfig = spawnerConfig;
            _spawnPositionPicker = spawnPositionPicker;
        }

        private void Start()
        {
            _timer = float.MaxValue;
            //   _spawnPositionPicker = new RandomOuterSquarePositionPicker(_spawnerConfig.SpawnPositionSideLenght);
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
            UFO ufo = Instantiate(_ufoData.Prefab, _spawnPositionPicker.GetNextPosition(), Quaternion.identity);
            ufo.transform.parent = container;
            ufo.InitializeData(_ufoData);

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