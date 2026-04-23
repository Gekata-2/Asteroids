using System;
using _Project.Scripts.Analytics;
using _Project.Scripts.AssetsProviding;
using _Project.Scripts.DataPersistence;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.Services;
using _Project.Scripts.Services.Pause;
using _Project.Scripts.Services.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Level.GameSession
{
    public class GameOverModel : IDisposable
    {
        public event Action GameOver;

        private readonly ISaveLoadService _saveLoadService;
        private readonly SaveProvider _saveProvider;

        private readonly IAnalytics _analytics;
        private readonly AnalyticsDataBuilder _analyticsDataBuilder;

        private readonly IAssetProvider _assetProvider;

        private readonly PauseService _pauseService;
        private readonly SceneLoader _sceneLoader;
        private readonly ExitGameService _exitGameService;
        private readonly GameSessionData _gameSessionData;
        private readonly PlayerConfig _playerConfig;
        private readonly LevelAssetsConfig _levelAssetsConfig;


        private Player.Player _player;

        public int Score => _gameSessionData.Score;

        public GameOverModel(
            ISaveLoadService saveLoadService, SaveProvider saveProvider,
            IAnalytics analytics, AnalyticsDataBuilder analyticsDataBuilder,
            IAssetProvider assetProvider,
            PauseService pauseService,
            SceneLoader sceneLoader,
            ExitGameService exitGameService,
            GameSessionData gameSessionData,
            LevelAssetsConfig levelAssetsConfig,
            PlayerConfig playerConfig)
        {
            _saveLoadService = saveLoadService;
            _saveProvider = saveProvider;

            _analytics = analytics;
            _analyticsDataBuilder = analyticsDataBuilder;

            _assetProvider = assetProvider;

            _pauseService = pauseService;
            _sceneLoader = sceneLoader;
            _exitGameService = exitGameService;
            _gameSessionData = gameSessionData;
            _levelAssetsConfig = levelAssetsConfig;
            _playerConfig = playerConfig;
        }


        public void SetPlayer(Player.Player player)
        {
            _player = player;
            _player.PlayerDead += OnPlayerDead;
        }

        private void OnPlayerDead()
        {
            HandlePlayerDead().Forget();
        }

        private async UniTask HandlePlayerDead()
        {
            _pauseService.PerformPause();
            GameOver?.Invoke();
            _analytics.LogGameOver(_analyticsDataBuilder.CreateGameOverData());
            await _saveLoadService.Save(_saveProvider.CreateSave());
        }

        public void RestartGame()
        {
            ReleaseUsedAssets();
            _sceneLoader.LoadLevelScene();
        }

        public void ExitGame()
        {
            ReleaseUsedAssets();
            _exitGameService.PerformExit();
        }

        public void ContinueGame()
        {
            if (_player.TryGetComponent(out WeaponsController weaponsController))
                weaponsController.ResetLaser();

            _player.ResetLife(_playerConfig.ImmunityTimespan);
            _pauseService.PerformResume();
        }


        private void ReleaseUsedAssets()
        {
            foreach (string assetKey in _levelAssetsConfig.UsedAssets)
                _assetProvider.Release(assetKey);
        }

        public void Dispose()
        {
            _player.PlayerDead -= OnPlayerDead;
        }
    }
}