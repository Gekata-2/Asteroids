using _Project.Scripts.Entities;
using _Project.Scripts.Services.EventBus;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Player
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