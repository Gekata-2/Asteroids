using _Project.Scripts.Extensions;
using _Project.Scripts.Services.AssetsManagement;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Scripts.Vfx
{
    public class VfxPool
    {
        private readonly ObjectPool<VfxPlayable> _pool;
        private readonly IAssetProvider _assetProvider;
        private readonly VfxPoolData _data;
        private readonly VfxFactory _factory;
        private Object _prefab;

        public VfxPool(VfxFactory factory, IAssetProvider assetProvider, VfxPoolData data)
        {
            _factory = factory;
            _assetProvider = assetProvider;
            _data = data;
         

            _pool = new ObjectPool<VfxPlayable>(
                CreateVfx,
                OnTakeVfxFromPool,
                OnReturnVfxToPool,
                OnDestroyVfx,
                collectionCheck: true,
                defaultCapacity: data.DefaultCapacity,
                maxSize: data.MaxSize);
        }

        public void PreWarm()
            => _pool.PreWarm(_data.PrewarmSize);

        public void FetchPrefab()
            => _assetProvider.TryGetAsset(AssetsNames.GetName(_data.VFX), out _prefab);

        public void Release(VfxPlayable vfx)
            => _pool.Release(vfx);

        public VfxPlayable Get()
            => _pool.Get();

        private VfxPlayable CreateVfx()
        {
            if (_prefab == null)
                FetchPrefab();

            VfxPlayable vfxPlayable = _factory.Create(_prefab);
            vfxPlayable.Initialize(this);
            Transform transform = vfxPlayable.transform;
            transform.parent = _data.Container;
            transform.position = _data.DefaultInactivePosition;
            return vfxPlayable;
        }

        private void OnDestroyVfx(VfxPlayable vfx)
        {
            Object.Destroy(vfx.gameObject);
        }

        private void OnReturnVfxToPool(VfxPlayable vfx)
        {
            vfx.transform.position = _data.DefaultInactivePosition;
            vfx.gameObject.SetActive(false);
        }

        private void OnTakeVfxFromPool(VfxPlayable vfx)
        {
            vfx.gameObject.SetActive(true);
        }
    }
}