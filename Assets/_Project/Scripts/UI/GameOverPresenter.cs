using System;
using _Project.Scripts.Level.GameSession;
using _Project.Scripts.Player;
using _Project.Scripts.Services.UI;
using _Project.Scripts.UI.Windows;
using Zenject;

namespace _Project.Scripts.UI
{
    public class GameOverPresenter : IInitializable, IDisposable
    {
        private readonly GameOverWindow _view;
        private readonly GameOverModel _model;
        private readonly UIManager _uiManager;
        private readonly IInput _input;

        public GameOverPresenter(GameOverWindow view, GameOverModel model, UIManager uiManager, IInput input)
        {
            _view = view;
            _model = model;
            _uiManager = uiManager;
            _input = input;
        }

        public void Initialize()
        {
            _model.GameOver += OnGameOver;

            _input.SubmitPerformed += OnSubmitPerformed;
            _input.CancelPerformed += OnCancelPerformed;
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