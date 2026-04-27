using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Services.AssetsProviding
{
    public interface IAssetProvider
    {
        UniTask Preload(params Asset[] assetsGroups);
        UniTask<Object> LoadAsync(string key);
        public bool TryGetAsset(string key, out Object asset);
        void Release(string key);
    }
}