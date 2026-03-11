using Player;

namespace Services
{
    public class UIManager
    {
        private UIState _current = UIState.None;
        public UIState CurrentState => _current;

        private readonly IInput _input;

        public UIManager(IInput input)
        {
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
        
        public bool CanOpenPause()
            => _current == UIState.None;
    }
}