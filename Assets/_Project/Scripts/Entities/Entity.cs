using _Project.Scripts.Services;
using UnityEngine;

namespace _Project.Scripts.Entities
{
    public abstract class Entity : MonoBehaviour, IPausable
    {
        public EntityConfig Data { get; private set; }
        public bool HasEnteredLevel { get; private set; } = false;

        public void InitializeData(EntityConfig entityData)
        {
            if (Data == null) 
                Data = entityData;
        }
        
        public abstract void SetPosition(Vector3 position);
        
        public abstract void Pause();
        public abstract void Resume();
        
        public void MarkEnteredLevel() 
            => HasEnteredLevel = true;
    }
}