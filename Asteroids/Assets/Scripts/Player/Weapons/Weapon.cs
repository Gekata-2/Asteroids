using UnityEngine;

namespace Player.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public abstract void TryShoot();
        public abstract void SetEnable(bool isEnabled);
    }
}