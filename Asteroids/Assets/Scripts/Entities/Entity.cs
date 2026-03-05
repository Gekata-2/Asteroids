using UnityEngine;

namespace Entities
{
    public abstract class Entity : MonoBehaviour
    {
        public abstract void Enable();
        public abstract void Disable();
        public abstract void SetPosition(Vector3 position);
    }
}