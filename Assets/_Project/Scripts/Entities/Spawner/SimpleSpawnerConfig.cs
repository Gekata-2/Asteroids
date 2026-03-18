using UnityEngine;

namespace _Project.Scripts.Entities.Spawner
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Create Asteroids Spawner Config",
        fileName = "Asteroids Spawner Config", order = 0)]
    class SimpleSpawnerConfig : ScriptableObject
    {
        [field: SerializeField] public float MinInterval { get; private set; } = 1f;
        [field: SerializeField] public float MaxInterval { get; private set; } = 3f;
        [field: SerializeField] public float StartDelay { get; private set; } = 2f;
        [field: SerializeField] public Vector2 SpawnPositionSize { get; private set; }
        [field: SerializeField] public Color GizmosColor { get; private set; }
    }
}