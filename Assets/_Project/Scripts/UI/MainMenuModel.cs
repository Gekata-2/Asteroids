using _Project.Scripts.Services;
using _Project.Scripts.Services.SceneManagement;

namespace _Project.Scripts.UI
{
    public class MainMenuModel
    {
        private readonly SceneLoader _sceneLoader;
        private readonly ExitGameService _exitGameService;

        public MainMenuModel(SceneLoader sceneLoader, ExitGameService exitGameService)
        {
            _sceneLoader = sceneLoader;
            _exitGameService = exitGameService;
        }

        public void ExitGame()
        {
            _exitGameService.PerformExit();
        }

        public void StartGame()
        {
            _sceneLoader.LoadLevelScene();
        }
    }
}