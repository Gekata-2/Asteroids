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
        private readonly GameSessionModel _playerModel;
        private readonly TimeService _timeService;

        // TODO
        private IAwardGiver<Entity> _entityDestroyedAwardGiver;
        private IAwardGiver<TimeLivedEvent> _timeLivedAwardGiver;
        private readonly ScoreConfig _scoreConfig;
        private readonly UfosController _ufosController;
        private readonly AsteroidsController _asteroidsController;

        public AwardsController(GameSessionModel playerModel,
            TimeService timeService, ScoreConfig scoreConfig
            , UfosController ufosController, AsteroidsController asteroidsController)
        {
            _playerModel = playerModel;
            _timeService = timeService;
            _scoreConfig = scoreConfig;
            _ufosController = ufosController;
            _asteroidsController = asteroidsController;
        }

        public void Initialize()
        {
            _entityDestroyedAwardGiver = new EntityDestroyedAwardGiver(_playerModel);
            _timeLivedAwardGiver = new TimeLivedAwardGiver(_playerModel, _scoreConfig.AliveDurationScore);

            _ufosController.UfoDestroyed += OnUfoDestroyed;
            _asteroidsController.AsteroidDestroyed += OnAsteroidDestroyed;
        }

        private void OnAsteroidDestroyed(Asteroid asteroid)
        {
            _entityDestroyedAwardGiver.GiveAwardFor(asteroid);
        }

        private void OnUfoDestroyed(UFO ufo)
        {
            _entityDestroyedAwardGiver.GiveAwardFor(ufo);
        }

        public void LateTick()
        {
            _timeLivedAwardGiver.GiveAwardFor(new TimeLivedEvent(_timeService.TimeElapsed));
        }

        public void Dispose()
        {
            _ufosController.UfoDestroyed -= OnUfoDestroyed;
            _asteroidsController.AsteroidDestroyed -= OnAsteroidDestroyed;
        }
    }
}