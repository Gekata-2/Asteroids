using System;
using _Project.Scripts.Services;
using _Project.Scripts.UI.Windows;
using Zenject;

namespace _Project.Scripts.UI
{
    public class PausePresenter : IInitializable, IDisposable
    {
        private readonly PauseInputHandler _pauseInput;
        private readonly PauseWindow _view;
        private readonly UIManager _uiManager;

        public PausePresenter(PauseWindow view, UIManager uiManager, PauseInputHandler pauseInput)
        {
            _view = view;
            _uiManager = uiManager;
            _pauseInput = pauseInput;
        }

        public void Initialize()
        {
            _pauseInput.GamePaused += OnGamePaused;
            _pauseInput.GameResumed += OnGameResumed;
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
            _pauseInput.GamePaused -= OnGamePaused;
            _pauseInput.GameResumed -= OnGameResumed;
        }
    }
}