using _Project.Scripts.Player;
using _Project.Scripts.UI;

namespace _Project.Scripts.Services.UI
{
    public class UIManager
    {
        private readonly PausePresenter _pausePresenter;

        private UIState _current = UIState.None;
        public UIState CurrentState => _current;

        private readonly IInput _input;

        public UIManager(PausePresenter pausePresenter, IInput input)
        {
            _pausePresenter = pausePresenter;
            _input = input;
        }

        public void SetState(UIState state)
        {
            if (state == _current)
                return;

            _current = state;

            if (_current == UIState.None)
                SwitchToPlayerActions();
            else
                SwitchToUIActions();
        }

        private void SwitchToPlayerActions()
        {
            _input.SetPlayerActionsEnabled(true);
            _input.SetUIActionsEnabled(false);
        }

        private void SwitchToUIActions()
        {
            _input.SetPlayerActionsEnabled(false);
            _input.SetUIActionsEnabled(true);
        }

        public void OpenPause()
        {
            if (_current == UIState.None)
            {
                _pausePresenter.OnShowPause();
                SetState(UIState.Pause);
            }
        }

        public void PerformCancel()
        {
            if (CurrentState == UIState.Pause)
            {
                _pausePresenter.OnHidePause();
                SetState(UIState.None);
            }
        }
    }
}