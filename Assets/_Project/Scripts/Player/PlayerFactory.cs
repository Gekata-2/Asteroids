using _Project.Scripts.Services.AssetsProviding;
using _Project.Scripts.Services.BeginGame;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Player
{
    public class PlayerFactory : IAssetFetcher
    {
        private readonly DiContainer _di;
        private readonly AssetsNames _assetsNames;
        private readonly IAssetProvider _assetProvider;

        private Object _prefab;

        public PlayerFactory(DiContainer di, AssetsNames assetsNames, IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _assetsNames = assetsNames;
            _di = di;
        }

        public void FetchAssets()
        {
            _assetProvider.TryGetAsset(_assetsNames.GetName(Asset.Player), out _prefab);
        }

        public Player Create(Vector3 position)
        {
            Player player = _di.InstantiatePrefabForComponent<Player>(
                _prefab,
                position,
                Quaternion.identity,
                null);

            return player;
        }
    }
}