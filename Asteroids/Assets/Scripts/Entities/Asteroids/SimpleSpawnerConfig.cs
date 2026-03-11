using UnityEngine;

namespace Entities.Asteroids
{
    [CreateAssetMenu(menuName = "Create Asteroids Spawner Config", fileName = "Asteroids Spawner Config", order = 0)]
    class SimpleSpawnerConfig : ScriptableObject
    {
        [SerializeField] private float minInterval = 1f;
        [SerializeField] private float maxInterval = 3f;
        [SerializeField] private float startDelay = 2f;
        [SerializeField] private float spawnPositionSideLenght;

        public float MinInterval => minInterval;
        public float MaxInterval => maxInterval;
        public float StartDelay => startDelay;
        public float SpawnPositionSideLenght => spawnPositionSideLenght;
    }
}