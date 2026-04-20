using System;
using _Project.Scripts.Player;
using _Project.Scripts.Services.BeginGame;
using _Project.Scripts.Services.Pause;
using _Project.Scripts.Services.UI;
using _Project.Scripts.UI.Windows;
using Zenject;

namespace _Project.Scripts.UI
{
    public class PausePresenter : IInitializable, IDisposable, IAssetFetcher
    {
        private const string WINDOW_ASSET_NAME = "pause_ui";

        private readonly PauseService _model;
        private readonly UIManager _uiManager;
        private readonly IInput _input;
        private readonly AssetsFactory _assetsFactory;
        
        private PauseWindow _view;

        public PausePresenter(PauseService model, UIManager uiManager, IInput input, AssetsFactory assetsFactory)
        {
            _model = model;
            _input = input;
            _uiManager = uiManager;
            _assetsFactory = assetsFactory;
        }

        public void Initialize()
        {
            _input.PausePerformed += OnPausePerformed;
            _input.CancelPerformed += OnCancelPerformed;
        }
        
        public void FetchAssets()
        {
            _view = _assetsFactory.Create<PauseWindow>(WINDOW_ASSET_NAME);
        }

        private void OnPausePerformed()
        {
            if (_uiManager.CurrentState == UIState.None)
            {
                _model.PerformPause();
                _view.Show();
                _uiManager.SetState(UIState.Pause);
            }
        }

        private void OnCancelPerformed()
        {
            if (_uiManager.CurrentState == UIState.Pause)
            {
                _model.PerformResume();
                _view.Hide();
                _uiManager.SetState(UIState.None);
            }
        }

        public void Dispose()
        {
            _input.PausePerformed -= OnPausePerformed;
            _input.CancelPerformed -= OnCancelPerformed;
        }
    }
}