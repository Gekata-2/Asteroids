using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Player.Weapons.Laser.UI
{
    public class LaserCooldownIcon : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;

        public void SetProgress(float value) 
            => _fillImage.fillAmount = value;
    }
}
