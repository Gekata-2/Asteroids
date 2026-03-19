using System;
using _Project.Scripts.Services;
using _Project.Scripts.UI.Windows;
using Zenject;

namespace _Project.Scripts.UI
{
    public class PausePresenter : IInitializable, IDisposable
    {
        private readonly PauseService _pauseService;
        private readonly PauseWindow _view;
        private readonly UIManager _uiManager;

        public PausePresenter(PauseService pauseService, PauseWindow view, UIManager uiManager)
        {
            _pauseService = pauseService;
            _view = view;
            _uiManager = uiManager;
        }

        public void Initialize()
        {
            _pauseService.GamePaused += OnGamePaused;
            _pauseService.GameResumed += OnGameResumed;
        }

        private void OnGamePaused()
        {
            if (_uiManager.CanOpenPause())
            {
                _view.Show();
                _uiManager.SetState(UIState.Pause);
            }
        }

        private void OnGameResumed()
        {
            _view.Hide();
            _uiManager.SetState(UIState.None);
        }

        public void Dispose()
        {
            _pauseService.GamePaused -= OnGamePaused;
            _pauseService.GameResumed -= OnGameResumed;
        }
    }
}