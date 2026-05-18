using System;
using _Project.Scripts.Player;
using _Project.Scripts.Services.AssetsManagement;
using _Project.Scripts.Services.UI;
using Zenject;

namespace _Project.Scripts.Services.Pause
{
    public class PausePresenter : IInitializable, IDisposable, IAssetFetcher
    {
        private readonly PauseModel _model;
        private readonly UIManager _uiManager;
        private readonly IInput _input;
        private readonly AssetsFactory _assetsFactory;
        private PauseWindow _view;

        public PausePresenter(PauseModel model, UIManager uiManager, IInput input, AssetsFactory assetsFactory)
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
            _input.SubmitPerformed += OnSubmitPerformed;
        }


        public void FetchAssets()
        {
            _view = _assetsFactory.Create<PauseWindow>(AssetsNames.GetName(Asset.PauseUI));
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

        private void OnSubmitPerformed()
        {
            if (_uiManager.CurrentState == UIState.Pause)
            {
                _model.PerformResume();
                _view.Hide();
                _uiManager.SetState(UIState.None);
            }
        }

        private void OnCancelPerformed()
        {
            if (_uiManager.CurrentState == UIState.Pause) 
                _model.ExitToMainMenu();
        }

        public void Dispose()
        {
            _input.PausePerformed -= OnPausePerformed;
            _input.CancelPerformed -= OnCancelPerformed;
            _input.SubmitPerformed -= OnSubmitPerformed;
        }
    }
}