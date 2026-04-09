using System;
using _Project.Scripts.DataPersistence;
using _Project.Scripts.Services;
using _Project.Scripts.Services.Pause;
using _Project.Scripts.Services.SceneManagement;

namespace _Project.Scripts.Level.GameSession
{
    public class GameOverModel : IDisposable
    {
        public event Action GameOver;
        private readonly ISaveLoadService _saveLoadService;

        private readonly PauseService _pauseService;
        private readonly SceneLoader _sceneLoader;
        private readonly ExitGameService _exitGameService;
        private readonly GameSessionData _gameSessionData;

        private Player.Player _player;

        public int Score => _gameSessionData.Score;

        public GameOverModel(
            ISaveLoadService saveLoadService,
            PauseService pauseService,
            SceneLoader sceneLoader,
            ExitGameService exitGameService,
            GameSessionData gameSessionData)
        {
            _saveLoadService = saveLoadService;
            _pauseService = pauseService;
            _sceneLoader = sceneLoader;
            _exitGameService = exitGameService;
            _gameSessionData = gameSessionData;
        }


        public void SetPlayer(Player.Player player)
        {
            _player = player;
            _player.PlayerDead += OnPlayerDead;
        }

        private async void OnPlayerDead()
        {
            _pauseService.PerformPause();
            GameOver?.Invoke();
            await _saveLoadService.Save(new SaveData(_gameSessionData.Score, _gameSessionData.TimeElapsed));
        }

        public void RestartGame()
            => _sceneLoader.LoadLevelScene();

        public void ExitGame()
            => _exitGameService.PerformExit();

        public void Dispose()
        {
            _player.PlayerDead -= OnPlayerDead;
        }
    }
}