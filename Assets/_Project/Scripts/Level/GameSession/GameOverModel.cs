using System;
using _Project.Scripts.Analytics;
using _Project.Scripts.AssetsProviding;
using _Project.Scripts.DataPersistence;
using _Project.Scripts.Services;
using _Project.Scripts.Services.Pause;
using _Project.Scripts.Services.SceneManagement;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Level.GameSession
{
    public class GameOverModel : IDisposable
    {
        public event Action GameOver;

        private readonly ISaveLoadService _saveLoadService;
        private readonly IAnalytics _analytics;
        private readonly IAssetProvider _assetProvider;
        private readonly SaveProvider _saveProvider;
        private readonly AnalyticsDataBuilder _analyticsDataBuilder;

        private readonly PauseService _pauseService;
        private readonly SceneLoader _sceneLoader;
        private readonly ExitGameService _exitGameService;
        private readonly GameSessionData _gameSessionData;
        private readonly LevelAssetsConfig _levelAssetsConfig;


        private Player.Player _player;

        public int Score => _gameSessionData.Score;

        public GameOverModel(
            PauseService pauseService,
            SceneLoader sceneLoader,
            ExitGameService exitGameService,
            GameSessionData gameSessionData,
            ISaveLoadService saveLoadService, SaveProvider saveProvider,
            IAnalytics analytics, AnalyticsDataBuilder analyticsDataBuilder,
            IAssetProvider assetProvider, LevelAssetsConfig levelAssetsConfig)
        {
            _pauseService = pauseService;
            _sceneLoader = sceneLoader;
            _exitGameService = exitGameService;
            _gameSessionData = gameSessionData;

            _saveLoadService = saveLoadService;
            _saveProvider = saveProvider;
            _analytics = analytics;
            _analyticsDataBuilder = analyticsDataBuilder;
            _levelAssetsConfig = levelAssetsConfig;
            _assetProvider = assetProvider;
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