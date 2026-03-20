using System;
using UnityEngine;

namespace _Project.Scripts.Entities.Asteroids.Configs
{
    [Serializable]
    public class AsteroidSpeedConfig
    {
        [field: SerializeField] public float Min { get; private set; } = 0.75f;
        [field: SerializeField] public float Max { get; private set; } = 1.25f;
    }
}