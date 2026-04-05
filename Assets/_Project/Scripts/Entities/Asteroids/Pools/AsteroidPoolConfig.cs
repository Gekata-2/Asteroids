using System;
using UnityEngine;

namespace _Project.Scripts.Entities.Asteroids.Pools
{
    [Serializable]
    public class AsteroidPoolConfig
    {
        [field: SerializeField] public Asteroid Prefab { get; private set; }
        [field: SerializeField] public int DefaultCapacity { get; private set; }
        [field: SerializeField] public int MaxSize { get; private set; }
    }
}