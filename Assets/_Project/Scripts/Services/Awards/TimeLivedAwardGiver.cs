using _Project.Scripts.Level.GameSession;

namespace _Project.Scripts.Services.Awards
{
    public class TimeLivedAwardGiver : IAwardGiver<TimeLivedEvent>
    {
        private readonly GameSessionModel _playerModel;
        private readonly GameSettings.AliveDurationScoreConfig _config;
        
        private float _lastAwardGivenTimeStamp;

        public TimeLivedAwardGiver(GameSessionModel playerModel, GameSettings.AliveDurationScoreConfig config)
        {
            _playerModel = playerModel;
            _config = config;
        }

        public void GiveAwardFor(TimeLivedEvent entityDestroyedEvent)
        {
            if (entityDestroyedEvent.TotalTime - _lastAwardGivenTimeStamp >= _config.TimeInterval)
            {
                _playerModel.AddScore(_config.ScoreValue);
                _lastAwardGivenTimeStamp = entityDestroyedEvent.TotalTime;
            }
        }
    }
}