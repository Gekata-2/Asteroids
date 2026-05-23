using _Project.Scripts.Services.AssetsManagement;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerFactory : IAssetFetcher
    {
        private readonly PlayerPrefabFactory _prefabFactory;
        private readonly IAssetProvider _assetProvider;

        private Object _prefab;

        public PlayerFactory(PlayerPrefabFactory prefabFactory, IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _prefabFactory = prefabFactory;
        }

        public void FetchAssets()
        {
            _assetProvider.TryGetAsset(AssetsNames.GetName(Asset.Player), out _prefab);
        }

        public Player Create(Vector3 position)
        {
            Player player = _prefabFactory.Create(_prefab);
            player.transform.position = position;

            return player;
        }
    }
}