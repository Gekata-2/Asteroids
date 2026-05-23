using _Project.Scripts.Services.AssetsManagement;
using UnityEngine;

namespace _Project.Scripts.GameOver
{
    public class GameOverWindowFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly GameOverWindowPrefabFactory _prefabFactory;

        public GameOverWindowFactory(IAssetProvider assetProvider, GameOverWindowPrefabFactory prefabFactory)
        {
            _assetProvider = assetProvider;
            _prefabFactory = prefabFactory;
        }

        public GameOverWindow Create()
        {
            _assetProvider.TryGetAsset(AssetsNames.GetName(Asset.GameOverUI), out Object prefab);
            return _prefabFactory.Create(prefab);
        }
    }
}