using _Project.Scripts.Entities;
using _Project.Scripts.Services.EventBus;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageble
    {
        [SerializeField] private bool isActive = true;

        private EventBus _eventBus;

        [Inject]
        private void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void TakeDamage(Damage damage)
        {
            if (isActive)
                _eventBus.Invoke(new PlayerDeadEvent(damage.Source));
        }
    }
}