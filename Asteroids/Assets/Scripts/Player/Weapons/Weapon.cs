using Services;
using UnityEngine;

namespace Player.Weapons
{
    public abstract class Weapon : MonoBehaviour, IPausable
    {
        public abstract void TryShoot();
        public abstract void Pause();
        public abstract void Resume();
    }
}