using TMPro;
using UnityEngine;

namespace UI
{
    public class TextScoreView : ScoreView
    {
        [SerializeField] private TMP_Text valueText;

        public override void SetScore(int value)
            => valueText.text = $"{value}";
    }
}