using System.Collections.Generic;
using _Project.Scripts.Level.GameSession;
using Zenject;

namespace _Project.Scripts.Entities.UFO
{
    public class UfosController : EntitiesController
    {
        private EntitiesContainer _entitiesContainer;
        private UfosSpawner _ufosSpawner;
        private GameSessionData _sessionData;
        private readonly List<Ufo> _ufos = new();

        [Inject]
        public void Construct(EntitiesContainer entitiesContainer,
            UfosSpawner ufosSpawner, GameSessionData sessionData)
        {
            _ufosSpawner = ufosSpawner;
            _entitiesContainer = entitiesContainer;
            _sessionData = sessionData;
        }
        
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
            ufo.Died += OnUfoDied;

            _ufos.Add(ufo);
            _entitiesContainer.AddEntity(ufo);
        }

        private void OnUfoDied(Ufo ufo)
        {
            UnsubscribeFromUfo(ufo);

            _ufos.Remove(ufo);
            _entitiesContainer.RemoveEntity(ufo);

            _sessionData.AddUfoDestroyed();
            
            NotifyAboutEntityDestroyed(ufo);
        }

        private void UnsubscribeFromUfo(Ufo ufo)
        {
            ufo.Died -= OnUfoDied;
        }
    }
}