using System;
using _Project.Scripts.Services.Awards;
using _Project.Scripts.UI;
using Zenject;

namespace _Project.Scripts.Level.GameSession
{
    public class PlayerScorePresenter : IInitializable, IDisposable
    {
        private readonly GameSessionData _model;
        private readonly ScoreView _view;
        private readonly ScoreConfig _scoreConfig;
        private readonly int _startingScore;

        public PlayerScorePresenter(GameSessionData model, ScoreView view, ScoreConfig scoreConfig)
        {
            _model = model;
            _view = view;
            _startingScore = scoreConfig.StartingScore;
        }

        public void Initialize()
        {
            _model.ScoreChanged += OnScoreChanged;
            _model.SetScore(_startingScore);
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