using System;
using _Project.Scripts.Services;

namespace _Project.Scripts.Level.GameSession
{
    public class GameSessionData
    {
        public event Action ScoreChanged;

        private readonly TimeService _timeService;

        public int Score { get; private set; }
        public int UfoDestroyed { get; private set; }
        public int AsteroidsDestroyed { get; private set; }
        
        public float TimeElapsed => _timeService.TimeElapsed;

        public GameSessionData(TimeService timeService)
        {
            _timeService = timeService;
        }

        public void AddScore(int value)
        {
            if (value == 0)
                return;

            Score += value;
            if (Score <= 0)
                Score = 0;

            ScoreChanged?.Invoke();
        }

        public void SetScore(int value)
        {
            if (value < 0)
                return;

            Score = value;
            ScoreChanged?.Invoke();
        }

        public void AddUfoDestroyed() 
            => UfoDestroyed++;
        
        public void AddAsteroidDestroyed()
            => AsteroidsDestroyed++;
    }
}