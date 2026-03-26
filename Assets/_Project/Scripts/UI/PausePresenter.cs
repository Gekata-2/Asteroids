using _Project.Scripts.Services.Pause;
using _Project.Scripts.UI.Windows;

namespace _Project.Scripts.UI
{
    public class PausePresenter
    {
        private readonly PauseWindow _view;
        private readonly PauseService _model;

        public PausePresenter(PauseWindow view,
            PauseService model)
        {
            _view = view;
            _model = model;
        }
        
        public void OnShowPause()
        {
            _model.PerformPause();
            _view.Show();
        }

        public void OnHidePause()
        {
            _model.PerformResume();
            _view.Hide();
        }
    }
}