using _Project.Scripts.Services.AssetsProviding;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI
{
    public class AssetsFactory
    {
        private readonly DiContainer _di;
        private readonly IAssetProvider _assetProvider;

        public AssetsFactory(DiContainer di, IAssetProvider assetProvider)
        {
            _di = di;
            _assetProvider = assetProvider;
        }

        public T Create<T>(string key)
        {
            _assetProvider.TryGetAsset(key, out Object prefab);
            GameObject instance = _di.InstantiatePrefab(prefab);
            return instance.GetComponent<T>();
        }
    }
}