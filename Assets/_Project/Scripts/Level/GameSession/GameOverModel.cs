using System;
using _Project.Scripts.Entities;
using _Project.Scripts.Player;
using _Project.Scripts.Services;
using _Project.Scripts.Services.Pause;
using _Project.Scripts.Services.SceneManagement;
using _Project.Scripts.Services.UI;
using _Project.Scripts.UI;
using Zenject;

namespace _Project.Scripts.Level.GameSession
{
    public class GameOverModel : IInitializable, IDisposable
    {
        public event Action GameOver;

        private readonly IInput _input;

        private readonly PauseService _pauseService;
        private readonly SceneLoader _sceneLoader;
        private readonly ExitGameService _exitGameService;
        private readonly GameSessionData _gameSessionData;

        private PlayerHealth _player;
        private readonly UIManager _uiManager;

        public int Score => _gameSessionData.Score;

        public GameOverModel(IInput input,
            PauseService pauseService,
            SceneLoader sceneLoader,
            ExitGameService exitGameService,
            UIManager uiManager, 
            GameSessionData gameSessionData)
        {
            _pauseService = pauseService;
            _sceneLoader = sceneLoader;
            _input = input;
            _exitGameService = exitGameService;
            _uiManager = uiManager;
            _gameSessionData = gameSessionData;
        }

        public void Initialize()
        {
            _input.UICancelPerformed += OnUICancelPerformed;
            _input.UISubmitPerformed += OnUISubmitPerformed;
            _player.PlayerDead += OnPlayerDead;
        }

        public void SetPlayer(PlayerHealth playerHealth)
        {
            _player = playerHealth;
            _player.PlayerDead += OnPlayerDead;
        }

        private void OnPlayerDead(Damage damage)
        {
            _pauseService.PerformPause();
            _uiManager.SetState(UIState.GameOver);
            GameOver?.Invoke();
        }

        private void OnUISubmitPerformed()
        {
            if (_uiManager.CurrentState == UIState.GameOver)
                _sceneLoader.LoadLevelScene();
        }

        private void OnUICancelPerformed()
        {
            if (_uiManager.CurrentState == UIState.GameOver)
                _exitGameService.PerformExit();
        }

        public void Dispose()
        {
            _input.UICancelPerformed -= OnUICancelPerformed;
            _input.UISubmitPerformed -= OnUISubmitPerformed;
            _player.PlayerDead -= OnPlayerDead;
        }
    }
}