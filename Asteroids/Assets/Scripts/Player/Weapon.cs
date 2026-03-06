using UnityEngine;

namespace Player
{
    public abstract class Weapon : MonoBehaviour
    {
        public abstract void TryShoot();
        public abstract void SetEnable(bool isEnabled);
    }
}