using System;
using System.Collections.Generic;
using _Project.Scripts.Entities;
using _Project.Scripts.Level;
using _Project.Scripts.Services;
using _Project.Scripts.Services.RemoteConfigs;
using Zenject;

namespace _Project.Scripts.Awards
{
    public class AwardsController : IInitializable, IDisposable, ILateTickable
    {
        private readonly GameSessionData _sessionData;
        private readonly TimeService _timeService;
        private readonly IConfigsProvider _configsProvider;
        private readonly List<EntitiesController> _entitiesControllers;

        private AliveDurationScoreConfig _aliveDurationScoreConfig;

        private float _lastAwardGivenTimeStamp;

        public AwardsController(GameSessionData sessionData, TimeService timeService,
            List<EntitiesController> entitiesControllers, IConfigsProvider configsProvider)
        {
            _sessionData = sessionData;
            _timeService = timeService;
            _entitiesControllers = entitiesControllers;
            _configsProvider = configsProvider;
        }

        public void Initialize()
        {
            ScoreConfig config = _configsProvider.GetValue<ScoreConfig>(ConfigsNames.Score);
            _aliveDurationScoreConfig = config.AliveDurationScore;
            foreach (EntitiesController entitiesController in _entitiesControllers)
                entitiesController.EntityDestroyed += OnEnemyEntityDestroyed;
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