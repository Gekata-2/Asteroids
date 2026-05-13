using System;
using _Project.Scripts.Awards;
using _Project.Scripts.Level;
using _Project.Scripts.Player.UI;
using _Project.Scripts.Services.AssetsManagement;
using _Project.Scripts.Services.RemoteConfigs;
using Zenject;

namespace _Project.Scripts.Player
{
    public class PlayerScorePresenter : IInitializable, IDisposable, IAssetFetcher
    {
        private readonly AssetsFactory _assetsFactory;
        private readonly IConfigsProvider _configsProvider;
        private readonly GameSessionData _model;
        
        private ScoreView _view;

        public PlayerScorePresenter(
            AssetsFactory assetsFactory,
            GameSessionData model,
            IConfigsProvider configsProvider)
        {
            _model = model;
            _configsProvider = configsProvider;
            _assetsFactory = assetsFactory;
        }

        public void Initialize()
        {
            ScoreConfig scoreConfig = _configsProvider.GetValue<ScoreConfig>(ConfigsNames.Score);
            _model.SetScore(scoreConfig.StartingScore);
            _model.ScoreChanged += OnScoreChanged;
        }

        public void FetchAssets()
        {
            _view = _assetsFactory.Create<ScoreView>(AssetsNames.GetName(Asset.ScoreUI));
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