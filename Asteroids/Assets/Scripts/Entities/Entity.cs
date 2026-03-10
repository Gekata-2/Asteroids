using Services;
using UnityEngine;

namespace Entities
{
    public abstract class Entity : MonoBehaviour, IPausable
    {
        public abstract void Enable();
        public abstract void Disable();
        public abstract void SetPosition(Vector3 position);
        
        public abstract void Pause();
        public abstract void Resume();
    }
}