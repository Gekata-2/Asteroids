using Entities;
using Services.EventBus;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerHealth : MonoBehaviour , IDamageble
    {
        [Inject]
        private EventBus _eventBus;
        
        public void TakeDamage(Damage damage)
        {
            _eventBus.Invoke(new PlayerDeadEvent(damage.Source));
        }
    }
}