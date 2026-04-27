using System;
using System.Collections.Generic;
using _Project.Scripts.Entities;
using _Project.Scripts.Level.GameSession;
using _Project.Scripts.Services.RemoteConfigs;
using Zenject;

namespace _Project.Scripts.Services.Awards
{
    public class AwardsController : IInitializable, IDisposable, ILateTickable, IConfigFetcher
    {
        private readonly GameSessionData _sessionData;
        private readonly TimeService _timeService;
        private readonly List<EntitiesController> _entitiesControllers;
        private AliveDurationScoreConfig _aliveDurationScoreConfig;

        private float _lastAwardGivenTimeStamp;

        public AwardsController(GameSessionData sessionData, TimeService timeService,
            List<EntitiesController> entitiesControllers)
        {
            _sessionData = sessionData;
            _timeService = timeService;
            _entitiesControllers = entitiesControllers;
        }

        public void Initialize()
        {
            foreach (EntitiesController entitiesController in _entitiesControllers)
                entitiesController.EntityDestroyed += OnEnemyEntityDestroyed;
        }

        public void FetchConfig(IConfigsProvider configsProvider)
        {
            ScoreConfig config = configsProvider.GetValue<ScoreConfig>(ConfigsNames.Score);
            _aliveDurationScoreConfig = config.AliveDurationScore;
        }

        private void OnEnemyEntityDestroyed(EnemyEntity entity)
            => _sessionData.AddScore(entity.Score);

        public void LateTick()
        {
            if (_aliveDurationScoreConfig == null)
                return;

            if (_timeService.TimeElapsed - _lastAwardGivenTimeStamp >= _aliveDurationScoreConfig.TimeInterval)
            {
                _sessionData.AddScore(_aliveDurationScoreConfig.ScoreValue);
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