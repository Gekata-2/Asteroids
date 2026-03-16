using _Project.Scripts.Services;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    public abstract class Weapon : MonoBehaviour, IPausable
    {
        public abstract void TryShoot();
        public abstract void Pause();
        public abstract void Resume();
    }
}