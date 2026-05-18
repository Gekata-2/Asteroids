using System;
using _Project.Scripts.Services.AssetsManagement;
using UnityEngine;

namespace _Project.Scripts.ObjectPools
{
    [Serializable]
    public class AssetPoolConfig : PoolConfig
    {
        [field: SerializeField] public Asset Asset { get; private set; }
    }
}