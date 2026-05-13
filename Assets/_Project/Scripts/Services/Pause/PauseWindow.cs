using UnityEngine;

namespace _Project.Scripts.Services.Pause
{
    public class PauseWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _content;

        public void Show() 
            => _content.SetActive(true);

        public void Hide() 
            => _content.SetActive(false);
    }
}