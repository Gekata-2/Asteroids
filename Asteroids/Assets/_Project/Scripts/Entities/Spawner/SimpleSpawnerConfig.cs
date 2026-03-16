using UnityEngine;

namespace _Project.Scripts.Entities.Spawner
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Create Asteroids Spawner Config", fileName = "Asteroids Spawner Config", order = 0)]
    class SimpleSpawnerConfig : ScriptableObject
    {
        [SerializeField] private float minInterval = 1f;
        [SerializeField] private float maxInterval = 3f;
        [SerializeField] private float startDelay = 2f;
        [field: SerializeField] public Vector2 SpawnPositionSize { get; private set; }
        [field: SerializeField] public Color GizmosColor { get; private set; }

        public float MinInterval => minInterval;
        public float MaxInterval => maxInterval;
        public float StartDelay => startDelay;
    }
}