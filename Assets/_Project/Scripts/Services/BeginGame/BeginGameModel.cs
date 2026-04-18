using System.Collections.Generic;
using _Project.Scripts.AssetsProviding;
using _Project.Scripts.Player;
using Cysharp.Threading.Tasks;
using ModestTree;
using UnityEngine;

namespace _Project.Scripts.Services.BeginGame
{
    public class BeginGameModel
    {
        private readonly List<IAssetFetcher> _fetchers;
        private readonly List<IGameStarter> _starters;
        private readonly IAssetProvider _assetProvider;
        private readonly PlayerFactory _playerFactory;

        public BeginGameModel(IAssetProvider assetProvider, PlayerFactory playerFactory,
            List<IGameStarter> starters = null,
            List<IAssetFetcher> fetchers = null)
        {
            if (fetchers == null) _fetchers = new List<IAssetFetcher>();
            if (starters == null) _starters = new List<IGameStarter>();

            _assetProvider = assetProvider;
            _playerFactory = playerFactory;
            _starters = starters;
            _fetchers = fetchers;
        }
        

        public async UniTask PreloadAssets(List<string> assetsGroups)
        {
            if (!assetsGroups.IsEmpty())
                await _assetProvider.Preload(assetsGroups.ToArray());
        }

        public void FetchAssets()
        {
            foreach (IAssetFetcher fetcher in _fetchers)
                fetcher.FetchAssets();
        }

        public Player.Player SpawnPlayer(Vector3 spawnPosition)
        {
            return _playerFactory.Create(spawnPosition);
        }

        public void BeginGame()
        {
            foreach (IGameStarter gameStarter in _starters)
                gameStarter.BeginGame();
        }
    }
}