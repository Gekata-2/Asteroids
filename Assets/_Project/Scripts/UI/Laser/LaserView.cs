using UnityEngine;

namespace _Project.Scripts.UI.Laser
{
    public abstract class LaserView : MonoBehaviour
    {
        public abstract void SetProgress(float timeLeft, float cooldownTime);
        public abstract void SetChargesCount(int charges);
    }
}