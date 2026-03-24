using System;
using System.Collections.Generic;
using _Project.Scripts.Entities.UFO.Configs;
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

        private IEnemyTargetable _ufosTarget;

        private readonly List<UFO> _ufos = new();

        [Inject]
        public void Construct(UfosSpawner ufosSpawner, EntitiesContainer entitiesContainer, UfoConfig ufoConfig)
        {
            _ufosSpawner = ufosSpawner;
            _entitiesContainer = entitiesContainer;
            _ufoConfig = ufoConfig;
        }

        public void SetTarget(IEnemyTargetable target)
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