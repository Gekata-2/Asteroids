using Player;
using Services;
using UnityEngine;

namespace Entities
{
    public abstract class Entity : MonoBehaviour, IPausable
    {
        public EntityData Data { get; private set; }
        public bool HasEnteredLevel { get; private set; } = false;

        public virtual void InitializeData(EntityData entityData)
        {
            if (Data == null) 
                Data = entityData;
        }
        
        public abstract void Enable();
        public abstract void Disable();
        public abstract void SetPosition(Vector3 position);
        
        public abstract void Pause();
        public abstract void Resume();
        public void MarkEnteredLevel() 
            => HasEnteredLevel = true;
    }
}