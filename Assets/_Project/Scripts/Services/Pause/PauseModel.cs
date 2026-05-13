using _Project.Scripts.Services.SceneManagement;

namespace _Project.Scripts.Services.Pause
{
    public class PauseModel
    {
        private readonly PauseService _service;
        private readonly SceneLoader _sceneLoader;

        public PauseModel(PauseService service, SceneLoader sceneLoader)
        {
            _service = service;
            _sceneLoader = sceneLoader;
        }

        public void PerformPause() 
            => _service.PerformPause();

        public void PerformResume() 
            => _service.PerformResume();

        public void ExitToMainMenu() 
            => _sceneLoader.LoadMainMenu();
    }
}