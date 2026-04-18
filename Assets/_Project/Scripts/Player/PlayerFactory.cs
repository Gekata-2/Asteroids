using _Project.Scripts.AssetsProviding;
using _Project.Scripts.Services.BeginGame;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Player
{
    public class PlayerFactory : IAssetFetcher
    {
        private const string ASSET_NAME = "player";

        private readonly DiContainer _di;
        private readonly IAssetProvider _assetProvider;

        private Object _prefab;

        public PlayerFactory(DiContainer di, IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _di = di;
        }

        public void FetchAssets()
        {
            _assetProvider.TryGetAsset(ASSET_NAME, out _prefab);
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