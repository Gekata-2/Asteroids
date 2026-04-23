using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.Windows
{
    public class GameOverWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _window;
        [SerializeField] private TMP_Text _score;

        public void Show(float score)
        {
            _score.text = $"{score}";
            _window.SetActive(true);
        }

        public void Hide() 
            => _window.SetActive(false);
    }
}