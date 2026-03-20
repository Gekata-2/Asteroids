using System;
using UnityEngine;

namespace _Project.Scripts.Entities.Asteroids.Configs
{
    [Serializable]
    public class AsteroidTorqueConfig
    {
        [field: SerializeField] public float Min { get; private set; } = 3f;
        [field: SerializeField] public float Max { get; private set; } = -3f;
    }
}