using System;
using _Project.Scripts.Entities;
using _Project.Scripts.Entities.Asteroids;
using _Project.Scripts.Entities.UFO;
using _Project.Scripts.Level.GameSession;
using Zenject;

namespace _Project.Scripts.Services.Awards
{
    public class AwardsController : IInitializable, IDisposable, ILateTickable
    {
        private readonly GameSessionData _sessionData;
        private readonly TimeService _timeService;
        private readonly ScoreConfig _scoreConfig;
        private readonly UfosController _ufosController;
        private readonly AsteroidsController _asteroidsController;

        private float _lastAwardGivenTimeStamp;

        public AwardsController(GameSessionData sessionData,
            TimeService timeService, ScoreConfig scoreConfig
            , UfosController ufosController, AsteroidsController asteroidsController)
        {
            _sessionData = sessionData;
            _timeService = timeService;
            _scoreConfig = scoreConfig;
            _ufosController = ufosController;
            _asteroidsController = asteroidsController;
        }

        public void Initialize()
        {
            _ufosController.UfoDestroyed += OnUfoDestroyed;
            _asteroidsController.AsteroidDestroyed += OnAsteroidDestroyed;
        }

        private void OnAsteroidDestroyed(Asteroid asteroid)
        {
            AddScoreForDestroyingEntity(asteroid);
        }

        private void OnUfoDestroyed(UFO ufo)
        {
            AddScoreForDestroyingEntity(ufo);
        }

        private void AddScoreForDestroyingEntity(Entity entity) 
            => _sessionData.AddScore(entity.Score);

        public void LateTick()
        {
            if (_timeService.TimeElapsed - _lastAwardGivenTimeStamp >= _scoreConfig.AliveDurationScore.TimeInterval)
            {
                _sessionData.AddScore(_scoreConfig.AliveDurationScore.ScoreValue);
                _lastAwardGivenTimeStamp = _timeService.TimeElapsed;
            }
        }

        public void Dispose()
        {
            _ufosController.UfoDestroyed -= OnUfoDestroyed;
            _asteroidsController.AsteroidDestroyed -= OnAsteroidDestroyed;
        }
    }
}