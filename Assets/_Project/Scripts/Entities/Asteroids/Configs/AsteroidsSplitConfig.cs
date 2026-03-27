using System;
using UnityEngine;

namespace _Project.Scripts.Entities.Asteroids.Configs
{
    [Serializable]
    public class AsteroidsSplitConfig
    {
        [field: SerializeField] public AsteroidConfig Config { get; private set; }
        [field: SerializeField] public int MinNewAsteroids { get; private set; } = 1;
        [field: SerializeField] public int MaxNewAsteroids { get; private set; } = 1;
        [field: SerializeField] public float Size { get; private set; } = 1;
    }
}