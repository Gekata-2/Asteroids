using System;
using _Project.Scripts.Services.Awards;
using _Project.Scripts.Services.BeginGame;
using _Project.Scripts.UI;
using Zenject;

namespace _Project.Scripts.Level.GameSession
{
    public class PlayerScorePresenter : IInitializable, IDisposable, IAssetFetcher
    {
        private const string WINDOW_ASSET_NAME = "score_ui";

        private readonly AssetsFactory _assetsFactory;
        private readonly GameSessionData _model;
        private readonly ScoreConfig _scoreConfig;
        private readonly int _startingScore;
        private ScoreView _view;

        public PlayerScorePresenter(AssetsFactory assetsFactory, GameSessionData model,
            ScoreConfig scoreConfig)
        {
            _model = model;
            _assetsFactory = assetsFactory;
            _startingScore = scoreConfig.StartingScore;
        }

        public void Initialize()
        {
            _model.ScoreChanged += OnScoreChanged;
            _model.SetScore(_startingScore);
        }

        public void FetchAssets()
        {
            _view = _assetsFactory.Create<ScoreView>(WINDOW_ASSET_NAME);
            _view.SetScore(_model.Score);
        }

        private void OnScoreChanged()
        {
            if (_view != null)
                _view.SetScore(_model.Score);
        }

        public void Dispose()
        {
            _model.ScoreChanged -= OnScoreChanged;
        }
    }
}