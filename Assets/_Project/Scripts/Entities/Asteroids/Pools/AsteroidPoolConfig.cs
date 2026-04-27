using System;
using _Project.Scripts.Services.AssetsProviding;
using UnityEngine;

namespace _Project.Scripts.Entities.Asteroids.Pools
{
    [Serializable]
    public class AsteroidPoolConfig
    {
        [field: SerializeField] public Asset Asset { get; private set; }
        [field: SerializeField] public int DefaultCapacity { get; private set; }
        [field: SerializeField] public int MaxSize { get; private set; }
    }
}