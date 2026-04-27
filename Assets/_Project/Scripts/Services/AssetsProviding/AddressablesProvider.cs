using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Services.AssetsProviding
{
    public class AddressablesProvider : IAssetProvider
    {
        private readonly Dictionary<string, Object> _cache = new();
        private readonly AssetsNames _assetsNames;

        public AddressablesProvider(AssetsNames assetsNames)
        {
            _assetsNames = assetsNames;
        }

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

        public async UniTask Preload(params Asset[] assetsKeys)
        {
            List<UniTask> loadTasks = new();

            foreach (Asset asset in assetsKeys)
                loadTasks.Add(LoadAsync(_assetsNames.GetName(asset)));

            await UniTask.WhenAll(loadTasks);
        }
    }
}