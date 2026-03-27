using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text valueText;

        public void SetScore(int value)
            => valueText.text = $"{value}";
    }
}