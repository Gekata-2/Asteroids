using System;
using _Project.Scripts.Player;
using _Project.Scripts.Services.Pause;
using _Project.Scripts.Services.UI;
using _Project.Scripts.UI.Windows;
using Zenject;

namespace _Project.Scripts.UI
{
    public class PausePresenter : IInitializable, IDisposable
    {
        private readonly PauseWindow _view;
        private readonly PauseService _model;
        private readonly UIManager _uiManager;
        private readonly IInput _input;

        public PausePresenter(PauseWindow view, PauseService model, UIManager uiManager, IInput input)
        {
            _view = view;
            _model = model;
            _input = input;
            _uiManager = uiManager;
        }

        public void Initialize()
        {
            _input.PausePerformed += OnPausePerformed;
            _input.CancelPerformed += OnCancelPerformed;
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