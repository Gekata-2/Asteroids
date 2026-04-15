using System;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.Laser
{
    public class LaserView : MonoBehaviour
    {
        [SerializeField] private LaserCooldownIcon _cooldownIcon;
        [SerializeField] private TMP_Text _chargesLeftText;

        public void SetProgress(float timeLeft, float cooldownTime)
        {
            if (Mathf.Approximately(cooldownTime, 0))
                Debug.LogException(new ArgumentException("Cooldown time equals zero"));

            _cooldownIcon.SetProgress(1f - timeLeft / cooldownTime);
        }

        public void SetChargesCount(int charges)
        {
            _chargesLeftText.text = $"x{charges}";
        }
    }
}