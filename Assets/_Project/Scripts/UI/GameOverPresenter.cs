using System;
using _Project.Scripts.Level.GameSession;
using _Project.Scripts.Services;
using _Project.Scripts.UI.Windows;
using Zenject;

namespace _Project.Scripts.UI
{
    public class GameOverPresenter : IInitializable, IDisposable
    {
        private readonly GameOverWindow _view;
        private readonly GameSessionModel _model;
        private readonly UIManager _uiManager;

        public GameOverPresenter(GameOverWindow view, GameSessionModel model, UIManager uiManager)
        {
            _view = view;
            _model = model;
            _uiManager = uiManager;
        }

        public void Initialize()
        {
            _model.GameOver += OnGameOver;
        }

        private void OnGameOver()
        {
            _view.Show(_model.Score);
            _uiManager.SetState(UIState.GameOver);
        }

        public void Dispose()
        {
            _model.GameOver -= OnGameOver;
        }
    }
}