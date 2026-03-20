using System;
using _Project.Scripts.Entities;
using _Project.Scripts.Player;
using _Project.Scripts.Services;
using _Project.Scripts.Services.SceneManagement;
using Zenject;

namespace _Project.Scripts.Level.GameSession
{
    public class GameSessionData
    {
        public event Action ScoreChanged;
        public int Score { get; private set; }

        public void AddScore(int value)
        {
            if (value == 0)
                return;

            Score += value;
            if (Score <= 0)
                Score = 0;

            ScoreChanged?.Invoke();
        }

        public void SetScore(int value)
        {
            if (value < 0)
                return;

            Score = value;
            ScoreChanged?.Invoke();
        }
    }

    public class GameSessionModel : IInitializable, IDisposable
    {
        public event Action GameOver;

        private readonly IInput _input;


        private readonly PauseService _pauseService;
        private readonly SceneLoader _sceneLoader;
        private readonly ExitGameService _exitGameService;

        public event Action ScoreChanged;
        public int Score { get; private set; }

        public void AddScore(int value)
        {
            if (value == 0)
                return;

            Score += value;
            if (Score <= 0)
                Score = 0;

            ScoreChanged?.Invoke();
        }

        public void SetScore(int value)
        {
            if (value < 0)
                return;

            Score = value;
            ScoreChanged?.Invoke();
        }

        private PlayerHealth _player;

        public GameSessionModel(IInput input,
            PauseService pauseService,
            SceneLoader sceneLoader,
            ExitGameService exitGameService)
        {
            _pauseService = pauseService;
            _sceneLoader = sceneLoader;
            _input = input;
            _exitGameService = exitGameService;
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
            GameOver?.Invoke();
        }

        private void OnUISubmitPerformed()
        {
            _sceneLoader.ReloadCurrentScene();
        }

        private void OnUICancelPerformed()
        {
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