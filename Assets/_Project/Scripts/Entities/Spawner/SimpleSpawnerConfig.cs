using UnityEngine;

namespace _Project.Scripts.Entities.Spawner
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Simple Spawner Config",
        fileName = "Asteroids Spawner Config", order = 0)]
    public class SimpleSpawnerConfig : ScriptableObject
    {
        [field: SerializeField] public SpawnerTimingConfig Timings { get; private set; }
        [field: SerializeField] public Vector2 SpawnPositionSize { get; private set; }
        [field: SerializeField] public Color GizmosColor { get; private set; }
    }
}