using UnityEngine;

namespace UI
{
    public abstract class PlayerStateView : MonoBehaviour
    {
        public abstract void SetX(float x);

        public abstract void SetY(float y);
        public abstract void SetPosition(Vector2 position);

        public abstract void SetAngle(float angle);

        public abstract void SetSpeed(float speed);
    }
}