using System;
using System.Collections.Generic;
using _Project.Scripts.Entities;
using _Project.Scripts.Level.GameSession;
using Zenject;

namespace _Project.Scripts.Services.Awards
{
    public class AwardsController : IInitializable, IDisposable, ILateTickable
    {
        private readonly GameSessionData _sessionData;
        private readonly TimeService _timeService;
        private readonly ScoreConfig _scoreConfig;

        private readonly List<EntitiesController> _entitiesControllers;

        private float _lastAwardGivenTimeStamp;

        public AwardsController(GameSessionData sessionData,
            TimeService timeService, ScoreConfig scoreConfig, List<EntitiesController> entitiesControllers)
        {
            _sessionData = sessionData;
            _timeService = timeService;
            _scoreConfig = scoreConfig;
            _entitiesControllers = entitiesControllers;
        }

        public void Initialize()
        {
            foreach (EntitiesController entitiesController in _entitiesControllers)
                entitiesController.EntityDestroyed += OnEnemyEntityDestroyed;
        }

        private void OnEnemyEntityDestroyed(EnemyEntity entity)
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
            foreach (EntitiesController entitiesController in _entitiesControllers)
                entitiesController.EntityDestroyed -= OnEnemyEntityDestroyed;
        }
    }
}