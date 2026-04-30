using _Project.Scripts.Entities.Spawner;
using UnityEngine;

namespace _Project.Scripts.Entities.Asteroids
{
    public class SimpleSpawnerConfig
    {
        public SpawnerTimingConfig Timings { get; set; }
        public Vector2 SpawnPositionSize { get; set; }
    }
}