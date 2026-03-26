using System;
using _Project.Scripts.Level.GameSession;
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

        public GameOverPresenter(GameOverWindow view, GameOverModel model)
        {
            _view = view;
            _model = model;
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