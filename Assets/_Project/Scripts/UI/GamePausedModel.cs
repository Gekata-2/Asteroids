using _Project.Scripts.Services;

namespace _Project.Scripts.UI
{
    public class GamePausedModel
    {
        private readonly PauseService _pauseService;

        public GamePausedModel(PauseService pauseService)
        {
            _pauseService = pauseService;
        }

        public void Pause() 
            => _pauseService.PerformPause();
        
        public void Resume() 
            => _pauseService.PerformResume();
    }
}