using System;
using System.Collections.Generic;
using _Project.Scripts.Level.BoundsHandling;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Entities.UFO
{
    public class UfosController : MonoBehaviour
    {
        public event Action<UFO> UfoDestroyed;

        private UfosSpawner _ufosSpawner;
        private EntitiesContainer _entitiesContainer;

        private IEnemyTargetable _ufosTarget;

        private readonly List<UFO> _ufos = new();
        private EntityOutOfBoundsController _entityOutOfBoundsController;

        [Inject]
        public void Construct(UfosSpawner ufosSpawner, EntitiesContainer entitiesContainer,
            EntityOutOfBoundsController entityOutOfBoundsController)
        {
            _ufosSpawner = ufosSpawner;
            _entitiesContainer = entitiesContainer;
            _entityOutOfBoundsController = entityOutOfBoundsController;
        }

        public void SetTarget(IEnemyTargetable target)
            => _ufosTarget = target;


        private void Start()
        {
            _ufosSpawner.UFOSpawned += OnUFOSpawned;
            _entityOutOfBoundsController.EntityOutOfOuterBoundsDestroyed += OnEntityOutOfOuterBoundsDestroyed;
            _ufosSpawner.StartSpawning();
        }

        private void OnDestroy()
        {
            _ufosSpawner.UFOSpawned -= OnUFOSpawned;
            _entityOutOfBoundsController.EntityOutOfOuterBoundsDestroyed -= OnEntityOutOfOuterBoundsDestroyed;

            foreach (UFO ufo in _ufos)
                ufo.Died -= OnUfoDied;

            _ufos.Clear();
        }

        private void OnEntityOutOfOuterBoundsDestroyed(Entity entity)
        {
            if (entity is UFO ufo)
            {
                ufo.Died -= OnUfoDied;
                _ufos.Remove(ufo);
            }
        }

        private void OnUFOSpawned(UFO ufo)
        {
            ufo.SetTarget(_ufosTarget);
            ufo.Died += OnUfoDied;

            _ufos.Add(ufo);
            _entitiesContainer.AddEntity(ufo);
        }

        private void OnUfoDied(UFO ufo)
        {
            ufo.Died -= OnUfoDied;

            _ufos.Remove(ufo);
            _entitiesContainer.RemoveEntity(ufo);

            UfoDestroyed?.Invoke(ufo);
        }
    }
}