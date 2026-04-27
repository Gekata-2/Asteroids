using System;
using _Project.Scripts.DataPersistence;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.Services;
using _Project.Scripts.Services.Analytics;
using _Project.Scripts.Services.AssetsProviding;
using _Project.Scripts.Services.Pause;
using _Project.Scripts.Services.RemoteConfigs;
using _Project.Scripts.Services.SceneManagement;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Level.GameSession
{
    public class GameOverModel : IDisposable, IConfigFetcher
    {
        public event Action GameOver;

        private readonly ISaveLoadService _saveLoadService;
        private readonly SaveProvider _saveProvider;

        private readonly IAnalytics _analytics;
        private readonly AnalyticsDataBuilder _analyticsDataBuilder;

        private readonly AssetsNames _assetsNames;
        private readonly IAssetProvider _assetProvider;

        private readonly PauseService _pauseService;
        private readonly SceneLoader _sceneLoader;
        private readonly ExitGameService _exitGameService;
        private readonly GameSessionData _gameSessionData;

        private readonly LevelAssetsConfig _levelAssetsConfig;
        private float _immunityTimespan = 1f;


        private Player.Player _player;

        public int Score => _gameSessionData.Score;

        public void FetchConfig(IConfigsProvider configsProvider)
        {
            PlayerConfig playerConfig = configsProvider.GetValue<PlayerConfig>(ConfigsNames.Player);
            _immunityTimespan = playerConfig.ImmunityTimespan;
        }

        public GameOverModel(
            ISaveLoadService saveLoadService, SaveProvider saveProvider,
            IAnalytics analytics, AnalyticsDataBuilder analyticsDataBuilder,
            AssetsNames assetsNames, IAssetProvider assetProvider,
            PauseService pauseService,
            SceneLoader sceneLoader,
            ExitGameService exitGameService,
            GameSessionData gameSessionData, LevelAssetsConfig levelAssetsConfig)
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
            _assetsNames = assetsNames;
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

            _player.ResetLife(_immunityTimespan);
            _pauseService.PerformResume();
        }


        private void ReleaseUsedAssets()
        {
            foreach (Asset asset in _levelAssetsConfig.UsedAssets)
                _assetProvider.Release(_assetsNames.GetName(asset));
        }

        public void Dispose()
        {
            _player.PlayerDead -= OnPlayerDead;
        }
    }
}