using System;
using _Project.Scripts.Level;
using _Project.Scripts.Meta.Analytics;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.Services;
using _Project.Scripts.Services.DataPersistence;
using _Project.Scripts.Services.Pause;
using _Project.Scripts.Services.RemoteConfigs;
using _Project.Scripts.Services.SceneManagement;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.GameOver
{
    public class GameOverModel : IInitializable, IDisposable
    {
        public event Action GameOver;

        private readonly SaveLoadService _saveLoadService;
        private readonly SaveProvider _saveProvider;

        private readonly IAnalyticsService _analyticsService;
        private readonly AnalyticsDataBuilder _analyticsDataBuilder;

        private readonly PauseService _pauseService;
        private readonly SceneLoader _sceneLoader;
        private readonly ExitGameService _exitGameService;
        private readonly GameSessionData _gameSessionData;
        private readonly IConfigsProvider _configsProvider;
        private float _immunityTimespan = 1f;

        private Player.Player _player;

        public int Score => _gameSessionData.Score;
        
        public GameOverModel(
            SaveLoadService saveLoadService, SaveProvider saveProvider,
            IAnalyticsService analyticsService, AnalyticsDataBuilder analyticsDataBuilder,
            PauseService pauseService,
            SceneLoader sceneLoader,
            ExitGameService exitGameService,
            GameSessionData gameSessionData, 
            IConfigsProvider configsProvider)
        {
            _saveLoadService = saveLoadService;
            _saveProvider = saveProvider;

            _analyticsService = analyticsService;
            _analyticsDataBuilder = analyticsDataBuilder;

            _pauseService = pauseService;
            _sceneLoader = sceneLoader;
            _exitGameService = exitGameService;
            _gameSessionData = gameSessionData;
            _configsProvider = configsProvider;
        }

        public void Initialize()
        {
            PlayerConfig playerConfig = _configsProvider.GetValue<PlayerConfig>(ConfigsNames.Player);
            _immunityTimespan = playerConfig.ImmunityTimespan;
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
            _analyticsService.LogGameOver(_analyticsDataBuilder.CreateGameOverData());
            SaveData saveData = _saveProvider.CreateSave();
            await _saveLoadService.Save(saveData);
        }

        public void RestartGame()
        {
            _sceneLoader.LoadLevelScene();
        }

        public void ExitGame()
        {
            _exitGameService.PerformExit();
        }

        public void ContinueGame()
        {
            if (_player.TryGetComponent(out WeaponsController weaponsController))
                weaponsController.ResetLaser();

            _player.ResetLife(_immunityTimespan);
            _pauseService.PerformResume();
        }

        public void ReturnToMainMenu()
        {
            _sceneLoader.LoadMainMenu();
        }

        public void Dispose()
        {
            if (_player==null) return;
            
            _player.PlayerDead -= OnPlayerDead;
        }
    }
}