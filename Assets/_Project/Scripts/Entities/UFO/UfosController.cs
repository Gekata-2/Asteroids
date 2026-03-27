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
        public event Action<Ufo> UfoDestroyed;

        private EntitiesContainer _entitiesContainer;
        private LevelBounds _levelBounds;
        private UfosSpawner _ufosSpawner;
        private UfoConfig _ufoConfig;

        private EnemyTarget _ufosTarget;

        private readonly List<Ufo> _ufos = new();

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
            _ufosSpawner.UfoSpawned += OnUfoSpawned;
            _ufosSpawner.StartSpawning();
        }

        private void OnDestroy()
        {
            _ufosSpawner.UfoSpawned -= OnUfoSpawned;

            foreach (Ufo ufo in _ufos)
                UnsubscribeFromUfo(ufo);

            _ufos.Clear();
        }

        private void OnUfoSpawned(Ufo ufo)
        {
            ufo.Initialize(_ufoConfig, _ufosTarget);

            if (ufo.TryGetComponent(out LevelBoundsHandler boundsHandler))
                boundsHandler.Initialize(_levelBounds);

            ufo.Died += OnUfoDied;

            _ufos.Add(ufo);
            _entitiesContainer.AddEntity(ufo);
        }

        private void OnUfoDied(Ufo ufo)
        {
            UnsubscribeFromUfo(ufo);

            _ufos.Remove(ufo);
            _entitiesContainer.RemoveEntity(ufo);

            UfoDestroyed?.Invoke(ufo);
        }

        private void UnsubscribeFromUfo(Ufo ufo)
        {
            ufo.Died -= OnUfoDied;
        }
    }
}