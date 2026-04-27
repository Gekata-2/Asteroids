using System;
using _Project.Scripts.Services.AssetsProviding;
using _Project.Scripts.Services.Awards;
using _Project.Scripts.Services.BeginGame;
using _Project.Scripts.Services.RemoteConfigs;
using _Project.Scripts.UI;
using Zenject;

namespace _Project.Scripts.Level.GameSession
{
    public class PlayerScorePresenter : IInitializable, IDisposable, IAssetFetcher, IConfigFetcher
    {
        private readonly AssetsFactory _assetsFactory;
        private readonly AssetsNames _assetsNames;
        private readonly GameSessionData _model;
        private ScoreView _view;

        public PlayerScorePresenter(AssetsFactory assetsFactory, GameSessionData model, AssetsNames assetsNames)
        {
            _model = model;
            _assetsNames = assetsNames;
            _assetsFactory = assetsFactory;
        }

        public void Initialize()
        {
            _model.ScoreChanged += OnScoreChanged;
        }

        public void FetchAssets()
        {
            _view = _assetsFactory.Create<ScoreView>(_assetsNames.GetName(Asset.ScoreUI));
            _view.SetScore(_model.Score);
        }

        public void FetchConfig(IConfigsProvider configsProvider)
        {
            ScoreConfig scoreConfig = configsProvider.GetValue<ScoreConfig>(ConfigsNames.Score);
            _model.SetScore(scoreConfig.StartingScore);
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