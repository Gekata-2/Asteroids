using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class TextScoreView : ScoreView
    {
        [SerializeField] private TMP_Text valueText;

        public override void SetScore(int value)
            => valueText.text = $"{value}";
    }
}