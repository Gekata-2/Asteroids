using System;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.Laser
{
    public class LaserIconView : LaserView
    {
        [SerializeField] private LaserCooldownIcon cooldownIcon;
        [SerializeField] private TMP_Text chargesLeftText;

        public override void SetProgress(float timeLeft, float cooldownTime)
        {
            if (Mathf.Approximately(cooldownTime, 0))
                Debug.LogException(new ArgumentException("Cooldown time equals zero"));

            cooldownIcon.SetProgress(1f - timeLeft / cooldownTime);
        }

        public override void SetChargesCount(int charges)
        {
            chargesLeftText.text = $"x{charges}";
        }
    }
}