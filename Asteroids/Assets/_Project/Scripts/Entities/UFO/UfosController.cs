using System.Collections.Generic;
using _Project.Scripts.Services.EventBus;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Entities.UFO
{
    public class UfosController : MonoBehaviour
    {
        private UfosSpawner _ufosSpawner;
        private List<UFO> _ufos;
        private EntitiesContainer _entitiesContainer;
        private EventBus _eventBus;
        private IEnemyTargetable _ufosTarget;

        [Inject]
        public void Construct(UfosSpawner ufosSpawner, EntitiesContainer entitiesContainer, EventBus eventBus)
        {
            _ufosSpawner = ufosSpawner;
            _entitiesContainer = entitiesContainer;
            _eventBus = eventBus;
        }

        public void SetTarget(IEnemyTargetable target)
            => _ufosTarget = target;

        private void Awake()
        {
            _ufos = new List<UFO>();
        }

        private void Start()
        {
            _ufosSpawner.UFOSpawned += UfosSpawner_OnUFOSpawned;
            _eventBus.Subscribe<EntityOutOfOuterBoundsDestroyedEvent>(OnEntityOutOfOuterBoundsDestroyed);
            _ufosSpawner.StartSpawning();
        }

        private void OnDestroy()
        {
            _ufosSpawner.UFOSpawned -= UfosSpawner_OnUFOSpawned;
            _eventBus.Unsubscribe<EntityOutOfOuterBoundsDestroyedEvent>(OnEntityOutOfOuterBoundsDestroyed);
          
            foreach (UFO ufo in _ufos)
                ufo.Died -= Ufo_OnDied;

            _ufos.Clear();
        }

        private void OnEntityOutOfOuterBoundsDestroyed(EntityOutOfOuterBoundsDestroyedEvent @event)
        {
            if (@event.Entity is UFO ufo)
            {
                ufo.Died -= Ufo_OnDied;
                _ufos.Remove(ufo);
            }
        }

        private void UfosSpawner_OnUFOSpawned(UFO ufo)
        {
            ufo.SetTarget(_ufosTarget);
            ufo.Died += Ufo_OnDied;

            _ufos.Add(ufo);
            _entitiesContainer.AddEntity(ufo);
        }

        private void Ufo_OnDied(UFO ufo)
        {
            ufo.Died -= Ufo_OnDied;

            _ufos.Remove(ufo);
            _entitiesContainer.RemoveEntity(ufo);

            _eventBus.Invoke(new EntityDestroyedEvent(ufo));
        }
    }
}