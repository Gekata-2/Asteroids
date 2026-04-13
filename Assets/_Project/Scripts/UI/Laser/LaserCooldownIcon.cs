using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Laser
{
    public class LaserCooldownIcon : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;

        public void SetProgress(float value) 
            => _fillImage.fillAmount = value;
    }
}
