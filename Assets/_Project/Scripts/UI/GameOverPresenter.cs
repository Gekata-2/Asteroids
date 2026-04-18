using System;
using _Project.Scripts.Level.GameSession;
using _Project.Scripts.Player;
using _Project.Scripts.Services.BeginGame;
using _Project.Scripts.Services.UI;
using _Project.Scripts.UI.Windows;
using Zenject;

namespace _Project.Scripts.UI
{
    public class GameOverPresenter : IInitializable, IDisposable, IAssetFetcher
    {
        private const string WINDOW_ASSET_NAME = "game_over_ui";

        private readonly AssetsFactory _assetsFactory;
        private readonly GameOverModel _model;
        private readonly UIManager _uiManager;
        private readonly IInput _input;
        
        private GameOverWindow _view;

        public GameOverPresenter(GameOverModel model, UIManager uiManager, IInput input,
            AssetsFactory assetsFactory)
        {
            _model = model;
            _uiManager = uiManager;
            _input = input;
            _assetsFactory = assetsFactory;
        }

        public void Initialize()
        {
            _model.GameOver += OnGameOver;

            _input.SubmitPerformed += OnSubmitPerformed;
            _input.CancelPerformed += OnCancelPerformed;
        }

        public void FetchAssets()
        {
            _view = _assetsFactory.Create<GameOverWindow>(WINDOW_ASSET_NAME);
        }

        private void OnSubmitPerformed()
        {
            if (_uiManager.CurrentState == UIState.GameOver)
                _model.RestartGame();
        }

        private void OnCancelPerformed()
        {
            if (_uiManager.CurrentState == UIState.GameOver)
                _model.ExitGame();
        }

        private void OnGameOver()
        {
            _view.Show(_model.Score);
            _uiManager.SetState(UIState.GameOver);
        }

        public void Dispose()
        {
            _model.GameOver -= OnGameOver;

            _input.SubmitPerformed -= OnSubmitPerformed;
            _input.CancelPerformed -= OnCancelPerformed;
        }
    }
}