using System;
using _Project.Scripts.UI;
using Zenject;

namespace _Project.Scripts.Level.GameSession
{
    public class PlayerScorePresenter : IInitializable, IDisposable
    {
        private const int START_SCORE = 0;

        private readonly GameSessionModel _model;
        private readonly ScoreView _view;

        public PlayerScorePresenter(GameSessionModel model, ScoreView view)
        {
            _model = model;
            _view = view;
        }

        public void Initialize()
        {
            _model.ScoreChanged += OnScoreChanged;
            _model.SetScore(START_SCORE);
        }

        private void OnScoreChanged()
        {
            _view.SetScore(_model.Score);
        }

        public void Dispose()
        {
            _model.ScoreChanged -= OnScoreChanged;
        }
    }
}