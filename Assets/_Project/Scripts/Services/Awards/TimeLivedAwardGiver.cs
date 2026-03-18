using _Project.Scripts.Player;

namespace _Project.Scripts.Services.Awards
{
    public class TimeLivedAwardGiver : IAwardGiver<TimeLivedEvent>
    {
        private readonly PlayerModel _playerModel;
        private float _lastAwardGivenTimeStamp;
        private readonly GameSettings.AliveDurationScoreConfig _config;

        public TimeLivedAwardGiver(PlayerModel playerModel, GameSettings.AliveDurationScoreConfig config)
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