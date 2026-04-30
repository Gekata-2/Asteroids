using System.Collections.Generic;
using _Project.Scripts.Player;
using _Project.Scripts.Services.AssetsProviding;
using _Project.Scripts.Services.RemoteConfigs;
using Cysharp.Threading.Tasks;
using ModestTree;
using UnityEngine;

namespace _Project.Scripts.Services.BeginGame
{
    public class BeginGameModel
    {
        private readonly List<IAssetFetcher> _assetFetchers;
        private readonly List<IGameStarter> _starters;
        private readonly List<IConfigFetcher> _configFetchers;
        private readonly IAssetProvider _assetProvider;
        private readonly PlayerFactory _playerFactory;
        private readonly IConfigsProvider _configsProvider;
        
        public BeginGameModel(IAssetProvider assetProvider, PlayerFactory playerFactory,
            IConfigsProvider configsProvider,
            List<IGameStarter> starters = null, 
            List<IAssetFetcher> assetFetchers = null,
            List<IConfigFetcher> configFetchers = null)
        {
            if (assetFetchers == null) _assetFetchers = new List<IAssetFetcher>();
            if (starters == null) _starters = new List<IGameStarter>();
            if (configFetchers == null) _configFetchers = new List<IConfigFetcher>();

            _assetProvider = assetProvider;
            _playerFactory = playerFactory;
            _configsProvider = configsProvider;
            _starters = starters;
            _assetFetchers = assetFetchers;
            _configFetchers = configFetchers;
        }


        public async UniTask PreloadAssets(List<Asset> assetsGroups)
        {
            if (!assetsGroups.IsEmpty())
                await _assetProvider.Preload(assetsGroups.ToArray());
        }

        public async UniTask ActivateConfigsData() 
            => await _configsProvider.ActivateData();

        public void FetchAssets()
        {
            foreach (IAssetFetcher fetcher in _assetFetchers)
                fetcher.FetchAssets();
        }

        public void FetchConfigs()
        {
            foreach (IConfigFetcher fetcher in _configFetchers)
                fetcher.FetchConfig(_configsProvider);
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