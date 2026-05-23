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
        private readonly IConfigsProvider _configsProvider;
        private readonly GameSessionData _model;
        private readonly ScoreViewFactory _viewFactory;
        
        private ScoreView _view;

        public PlayerScorePresenter(GameSessionData model,
            IConfigsProvider configsProvider, ScoreViewFactory viewFactory)
        {
            _model = model;
            _configsProvider = configsProvider;
            _viewFactory = viewFactory;
        }

        public void Initialize()
        {
            ScoreConfig scoreConfig = _configsProvider.GetValue<ScoreConfig>(ConfigsNames.Score);
            _model.SetScore(scoreConfig.StartingScore);
            _model.ScoreChanged += OnScoreChanged;
        }

        public void FetchAssets()
        {
            _view = _viewFactory.Create();
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