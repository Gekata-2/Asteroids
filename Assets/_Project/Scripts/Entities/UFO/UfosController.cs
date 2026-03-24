using System;
using System.Collections.Generic;
using _Project.Scripts.Entities.UFO.Configs;
using _Project.Scripts.Level.BoundsHandling;
using _Project.Scripts.Player;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Entities.UFO
{
    public class UfosController : MonoBehaviour
    {
        public event Action<UFO> UfoDestroyed;

        private UfosSpawner _ufosSpawner;
        private EntitiesContainer _entitiesContainer;
        private UfoConfig _ufoConfig;
        private LevelBounds _levelBounds;

        private EnemyTarget _ufosTarget;

        private readonly List<UFO> _ufos = new();

        [Inject]
        public void Construct(EntitiesContainer entitiesContainer,
            UfosSpawner ufosSpawner, UfoConfig ufoConfig,
            LevelBounds levelBounds)
        {
            _ufosSpawner = ufosSpawner;
            _entitiesContainer = entitiesContainer;
            _ufoConfig = ufoConfig;
            _levelBounds = levelBounds;
        }

        public void SetTarget(EnemyTarget target)
            => _ufosTarget = target;


        private void Start()
        {
            _ufosSpawner.UFOSpawned += OnUFOSpawned;
            _ufosSpawner.StartSpawning();
        }

        private void OnDestroy()
        {
            _ufosSpawner.UFOSpawned -= OnUFOSpawned;

            foreach (UFO ufo in _ufos)
                ufo.Died -= OnUfoDied;

            _ufos.Clear();
        }

        private void OnUFOSpawned(UFO ufo)
        {
            ufo.Initialize(_ufoConfig, _ufosTarget);
            if (ufo.TryGetComponent(out LevelBoundsHandler boundsHandler))
            {
                boundsHandler.Initialize(_levelBounds);
                boundsHandler.Destroyed += OnUfoDestroyedAfterCrossingOuterBounds;
            }

            ufo.Died += OnUfoDied;

            _ufos.Add(ufo);
            _entitiesContainer.AddEntity(ufo);
        }

        private void OnUfoDestroyedAfterCrossingOuterBounds(Entity entity)
        {
            HandleUfoDestroyed(entity as UFO);
        }

        private void OnUfoDied(UFO ufo)
        {
            HandleUfoDestroyed(ufo);
        }

        private void HandleUfoDestroyed(UFO ufo)
        {
            ufo.Died -= OnUfoDied;

            _ufos.Remove(ufo);
            _entitiesContainer.RemoveEntity(ufo);

            if (ufo.TryGetComponent(out LevelBoundsHandler boundsHandler))
                boundsHandler.Destroyed -= OnUfoDestroyedAfterCrossingOuterBounds;

            UfoDestroyed?.Invoke(ufo);
        }
    }
}