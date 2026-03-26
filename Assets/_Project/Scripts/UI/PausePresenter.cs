using _Project.Scripts.UI.Windows;

namespace _Project.Scripts.UI
{
    public class PausePresenter
    {
        private readonly PauseWindow _view;
        private readonly GamePausedModel _model;

        public PausePresenter(PauseWindow view,
            GamePausedModel model)
        {
            _view = view;
            _model = model;
        }


        public void OnShowPause()
        {
            _model.Pause();
            _view.Show();
        }

        public void OnHidePause()
        {
            _model.Resume();
            _view.Hide();
        }
    }
}