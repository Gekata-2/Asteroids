using System;
using UnityEngine;

namespace _Project.Scripts.Entities.Spawner
{
    [Serializable]
    public class SpawnerTimingConfig
    {
        [field: SerializeField] public float MinInterval { get; private set; } = 1f;
        [field: SerializeField] public float MaxInterval { get; private set; } = 3f;
        [field: SerializeField] public float StartDelay { get; private set; } = 2f;
    }
}