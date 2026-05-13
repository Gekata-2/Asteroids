using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Services.AssetsManagement
{
    public class AddressablesProvider : IAssetProvider
    {
        private readonly Dictionary<string, Object> _cache = new();

        public async UniTask<Object> LoadAsync(string key)
        {
            if (_cache.TryGetValue(key, out Object asset))
                return asset;
            asset = await Addressables.LoadAssetAsync<Object>(key).Task.AsUniTask();
            _cache[key] = asset;
            return asset;
        }

        public bool TryGetAsset(string key, out Object asset)
        {
            return _cache.TryGetValue(key, out asset);
        }

        public void Release(string key)
        {
            if (_cache.TryGetValue(key, out Object obj))
            {
                Addressables.Release(obj);
                _cache.Remove(key);
            }
        }

        public async UniTask Preload(params string[] assetKeys)
        {
            List<UniTask> loadTasks = new();

            foreach (string asset in assetKeys)
                loadTasks.Add(LoadAsync(asset));

            await UniTask.WhenAll(loadTasks);
        }
    }
}