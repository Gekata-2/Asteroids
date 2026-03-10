using Asteroids;
using Entities;
using Services;
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
            Debug.Log("Death");
            _eventBus.Invoke(new PlayerDeadEvent(damage.Source));
        }
    }
}