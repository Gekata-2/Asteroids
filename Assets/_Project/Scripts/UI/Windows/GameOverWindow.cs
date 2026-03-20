using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.Windows
{
    public class GameOverWindow : MonoBehaviour
    {
        [SerializeField] private GameObject window;
        [SerializeField] private TMP_Text score;
        
        public void Show(float score)
        {
            this.score.text = $"{score}";
            window.SetActive(true);
        }
    }
}