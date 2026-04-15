using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _valueText;

        public void SetScore(int value)
            => _valueText.text = $"{value}";
    }
}