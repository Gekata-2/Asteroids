using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Laser
{
    public class LaserCooldownIcon : MonoBehaviour
    {
        [SerializeField] private Image fillImage;

        public void SetProgress(float value) 
            => fillImage.fillAmount = value;
    }
}
