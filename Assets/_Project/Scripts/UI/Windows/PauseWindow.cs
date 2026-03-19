using UnityEngine;

namespace _Project.Scripts.UI.Windows
{
    public class PauseWindow : MonoBehaviour
    {
        [SerializeField] private GameObject content;

        public void Show()
        {
            content.SetActive(true);
        }

        public void Hide()
        {
            content.SetActive(false);
        }
    }
}