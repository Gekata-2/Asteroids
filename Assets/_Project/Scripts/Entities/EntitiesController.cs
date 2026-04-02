using System;
using UnityEngine;

namespace _Project.Scripts.Entities
{
    public class EntitiesController : MonoBehaviour
    {
        public event Action<EnemyEntity> EntityDestroyed;

        protected void NotifyAboutEntityDestroyed(EnemyEntity entity) 
            => EntityDestroyed?.Invoke(entity);
    }
}