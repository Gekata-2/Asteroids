using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class PlayerStateView : MonoBehaviour
    {
        private const string DEGREE_UNICOD = "\u00b0";

        [SerializeField] private TMP_Text xCoordText;
        [SerializeField] private TMP_Text yCoordText;
        [SerializeField] private TMP_Text angleText;
        [SerializeField] private TMP_Text speedText;
        
        public void Initialize(Vector2 position, float angle, float speed)
        {
            SetPosition(position);
            SetAngle(angle);
            SetSpeed(speed);
        }
        
        public void SetPosition(Vector2 position)
        {
            xCoordText.text = $"{position.x:0.#}";
            yCoordText.text = $"{position.y:0.#}";
        }
        
        public void SetAngle(float angle)
            => angleText.text = $"{angle:0.#}{DEGREE_UNICOD}";

        public void SetSpeed(float speed)
            => speedText.text = $"{speed:0.#}";
    }
}