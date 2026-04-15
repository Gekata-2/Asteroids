using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class PlayerStateView : MonoBehaviour
    {
        private const string DEGREE_UNICOD = "\u00b0";

        [SerializeField] private TMP_Text _xCoordText;
        [SerializeField] private TMP_Text _yCoordText;
        [SerializeField] private TMP_Text _angleText;
        [SerializeField] private TMP_Text _speedText;
        
        public void Initialize(Vector2 position, float angle, float speed)
        {
            SetPosition(position);
            SetAngle(angle);
            SetSpeed(speed);
        }
        
        public void SetPosition(Vector2 position)
        {
            _xCoordText.text = $"{position.x:0.#}";
            _yCoordText.text = $"{position.y:0.#}";
        }
        
        public void SetAngle(float angle)
            => _angleText.text = $"{angle:0.#}{DEGREE_UNICOD}";

        public void SetSpeed(float speed)
            => _speedText.text = $"{speed:0.#}";
    }
}