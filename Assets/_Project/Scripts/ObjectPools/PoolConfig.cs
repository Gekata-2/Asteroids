using System;
using UnityEngine;

namespace _Project.Scripts.ObjectPools
{
    [Serializable]
    public class PoolConfig
    {
        [field: SerializeField] public int DefaultCapacity { get; private set; }
        [field: SerializeField] public int MaxSize { get; private set; }
        [field: SerializeField] public int PrewarmSize { get; private set; }
    }
}