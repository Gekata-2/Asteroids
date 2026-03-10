using System;
using Player;
using Services.EventBus;
using Zenject;

namespace Services.Awards
{
    public class AwardsController : IInitializable, IDisposable, ILateTickable
    {
        private readonly EventBus.EventBus _eventBus;
        private readonly PlayerModel _playerModel;
        private readonly TimeService _timeService;

        private IAwardGiver<EntityDestroyedEvent> _entityDestroyedAwardGiver;
        private IAwardGiver<TimeLivedEvent> _timeLivedAwardGiver;

        public AwardsController(EventBus.EventBus eventBus, PlayerModel playerModel, TimeService timeService)
        {
            _eventBus = eventBus;
            _playerModel = playerModel;
            _timeService = timeService;
        }

        public void Initialize()
        {
            _eventBus.Subscribe<EntityDestroyedEvent>(OnEntityDestroyed);
            _entityDestroyedAwardGiver = new EntityDestroyedAwardGiver(_playerModel);
            _timeLivedAwardGiver = new TimeLivedAwardGiver(_playerModel);
        }

        private void OnEntityDestroyed(EntityDestroyedEvent @event)
        {
            _entityDestroyedAwardGiver.GiveAwardFor(@event);
        }


        public void Dispose()
        {
            _eventBus.Unsubscribe<EntityDestroyedEvent>(OnEntityDestroyed);
        }

        public void LateTick()
        {
            _timeLivedAwardGiver.GiveAwardFor(new TimeLivedEvent(_timeService.TimeElapsed));
        }
    }
}