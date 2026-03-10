using System.Collections.Generic;
using Services.EventBus;
using UnityEngine;
using Zenject;

namespace Entities.UFO
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
            _ufosSpawner.StartSpawning();
        }

        private void UfosSpawner_OnUFOSpawned(UFO ufo)
        {
            ufo.SetTarget(_ufosTarget);
            _ufos.Add(ufo);
            _entitiesContainer.AddEntity(ufo);
        }

        private void OnDestroy()
        {
            _ufosSpawner.UFOSpawned -= UfosSpawner_OnUFOSpawned;
        }


        // private void SubscribeToAsteroid(Asteroid asteroid)
        // {
        //     asteroid.CollidedWithBullet += Asteroid_OnCollidedWithBullet;
        //     asteroid.SweepedByLaser += Asteroid_OnSweepedByLaser;
        // }
        //
        // private void UnsubscribeFromAsteroid(Asteroid asteroid)
        // {
        //     asteroid.CollidedWithBullet -= Asteroid_OnCollidedWithBullet;
        //     asteroid.SweepedByLaser -= Asteroid_OnSweepedByLaser;
        // }
        //
        // private void Asteroid_OnCollidedWithBullet(Asteroid asteroid)
        // {
        //     UnsubscribeFromAsteroid(asteroid);
        //
        //     Queue<AsteroidsChainData> chain = asteroid.SplitChain;
        //     DestroyAsteroid(asteroid);
        //
        //     if (chain.IsEmpty())
        //         return;
        //     _ufosSpawner.SpawnAsteroidsFromPosition(chain, asteroid.transform.position);
        //     chain.Dequeue();
        // }
        //
        // private void DestroyAsteroid(Asteroid asteroid)
        // {
        //     asteroid.Die();
        //     _asteroids.Remove(asteroid);
        //     _entitiesContainer.RemoveEntity(asteroid);
        //     _eventBus.Invoke(new EntityDestroyedEvent(asteroid));
        // }
        //
        // private void Asteroid_OnSweepedByLaser(Asteroid asteroid)
        // {
        //     DestroyAsteroid(asteroid);
        // }

        public void Pause()
        {
            foreach (UFO ufo in _ufos)
                ufo.Pause();
        }

        public void Resume()
        {
            foreach (UFO ufo in _ufos)
                ufo.Resume();
        }
    }
}