using Player;

namespace Services.Awards
{
    public class TimeLivedAwardGiver : IAwardGiver<TimeLivedEvent>
    {
        private const float SCORE_COOLDOWN = 1f;
        private const int SCORE_AWARD = 1;

        private readonly PlayerModel _playerModel;
        private float _lastAwardGivenTimeStamp;

        public TimeLivedAwardGiver(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        public void GiveAwardFor(TimeLivedEvent entityDestroyedEvent)
        {
            if (entityDestroyedEvent.TotalTime - _lastAwardGivenTimeStamp >= SCORE_COOLDOWN)
            {
                _playerModel.AddScore(SCORE_AWARD);
                _lastAwardGivenTimeStamp = entityDestroyedEvent.TotalTime;
            }
        }
    }
}