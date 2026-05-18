using System.Collections.Generic;
using _Project.Scripts.Player;
using _Project.Scripts.Services.AssetsManagement;
using _Project.Scripts.Services.RemoteConfigs;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Services.BeginGame
{
    public class BeginGameModel
    {
        private readonly List<IAssetFetcher> _assetFetchers;
        private readonly List<IGameStarter> _starters;

        private readonly PlayerFactory _playerFactory;
        private readonly IConfigsProvider _configsProvider;

        public BeginGameModel(PlayerFactory playerFactory,
            IConfigsProvider configsProvider,
            List<IGameStarter> starters = null,
            List<IAssetFetcher> assetFetchers = null)
        {
            if (assetFetchers == null) _assetFetchers = new List<IAssetFetcher>();
            if (starters == null) _starters = new List<IGameStarter>();

            _playerFactory = playerFactory;
            _configsProvider = configsProvider;
            _starters = starters;
            _assetFetchers = assetFetchers;
        }


        public async UniTask ActivateConfigsData()
            => await _configsProvider.ActivateData();

        public void FetchAssets()
        {
            foreach (IAssetFetcher fetcher in _assetFetchers)
                fetcher.FetchAssets();
        }

        public Player.Player SpawnPlayer(Vector3 spawnPosition)
            => _playerFactory.Create(spawnPosition);

        public void BeginGame()
        {
            foreach (IGameStarter gameStarter in _starters)
                gameStarter.BeginGame();
        }
    }
}