using TMPro;
using UnityEngine;

namespace UI
{
    public class TextPlayerDataView : PlayerDataView
    {
        [SerializeField] private TMP_Text xCoordText;
        [SerializeField] private TMP_Text yCoordText;
        [SerializeField] private TMP_Text angleCoordText;
        [SerializeField] private TMP_Text speedCoordText;

        public override void SetX(float x)
        {
            xCoordText.text = $"{x:0.#}";
        }

        public override void SetY(float y)
        {
            yCoordText.text = $"{y:0.#}";
        }

        public override void SetPosition(Vector2 position)
        {
            SetX(position.x);
            SetY(position.y);
        }

        public override void SetAngle(float angle)
        {
            angleCoordText.text = $"{angle:0.#}";
        }

        public override void SetSpeed(float speed)
        {
            speedCoordText.text = $"{speed:0.#}";
        }
    }
}