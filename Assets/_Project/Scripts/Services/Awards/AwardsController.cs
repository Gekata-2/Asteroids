using System;
using _Project.Scripts.Level.GameSession;
using _Project.Scripts.Services.EventBus;
using Zenject;

namespace _Project.Scripts.Services.Awards
{
    public class AwardsController : IInitializable, IDisposable, ILateTickable
    {
        private readonly EventBus.EventBus _eventBus;
        private readonly GameSessionModel _playerModel;
        private readonly TimeService _timeService;

        private IAwardGiver<EntityDestroyedEvent> _entityDestroyedAwardGiver;
        private IAwardGiver<TimeLivedEvent> _timeLivedAwardGiver;
        private readonly GameSettings _gameSettings;

        public AwardsController(EventBus.EventBus eventBus, GameSessionModel playerModel,
            TimeService timeService, GameSettings gameSettings)
        {
            _eventBus = eventBus;
            _playerModel = playerModel;
            _timeService = timeService;
            _gameSettings = gameSettings;
        }

        public void Initialize()
        {
            _eventBus.Subscribe<EntityDestroyedEvent>(OnEntityDestroyed);
            _entityDestroyedAwardGiver = new EntityDestroyedAwardGiver(_playerModel);
            _timeLivedAwardGiver = new TimeLivedAwardGiver(_playerModel, _gameSettings.AliveDurationScore);
        }

        private void OnEntityDestroyed(EntityDestroyedEvent @event)
            => _entityDestroyedAwardGiver.GiveAwardFor(@event);

        public void LateTick()
        {
            _timeLivedAwardGiver.GiveAwardFor(new TimeLivedEvent(_timeService.TimeElapsed));
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<EntityDestroyedEvent>(OnEntityDestroyed);
        }
    }
}