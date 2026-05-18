using System.Collections.Generic;
using _Project.Scripts.ObjectPools;
using _Project.Scripts.Services.AssetsManagement;
using _Project.Scripts.Services.BeginGame;
using UnityEngine;

namespace _Project.Scripts.Vfx
{
    public class VfxSystem : IGameStarter, IAssetFetcher
    {
        private const string CONTAINER_NAME = "Vfx Container";

        private readonly Dictionary<VFX, VfxPool> _pools = new();

        public VfxSystem(PoolsConfigs configs, VfxPoolFactory poolFactory)
        {
            GameObject container = new GameObject(CONTAINER_NAME);
            foreach (var pair in configs.VfxPools)
            {
                PoolConfig config = pair.Value;
                VfxPool pool = poolFactory.Create(
                    new VfxPoolData(config.PrewarmSize, config.DefaultCapacity, config.MaxSize,
                        pair.Key,
                        container.transform,
                        configs.InactiveObjectPosition));
                _pools.Add(pair.Key, pool);
            }
        }

        public void FetchAssets()
        {
            foreach (VfxPool pool in _pools.Values)
                pool.FetchPrefab();
        }

        public void BeginGame()
        {
            foreach (VfxPool pool in _pools.Values)
                pool.PreWarm();
        }

        public void PlayVfx(VFX vfx, Vector2 position)
        {
            VfxPlayable playable = _pools[vfx].Get();
            playable.transform.position = position;
        }
    }
}