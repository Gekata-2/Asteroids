using _Project.Scripts.Services.AssetsManagement;
using UnityEngine;

namespace _Project.Scripts.Player.UI
{
    public class PlayerStateViewFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly PlayerStateViewPrefabFactory _prefabFactory;

        public PlayerStateViewFactory(IAssetProvider assetProvider, PlayerStateViewPrefabFactory prefabFactory)
        {
            _assetProvider = assetProvider;
            _prefabFactory = prefabFactory;
        }

        public PlayerStateView Create()
        {
            _assetProvider.TryGetAsset(AssetsNames.GetName(Asset.PlayerStateUI), out Object prefab);
            return _prefabFactory.Create(prefab);
        }
    }
}