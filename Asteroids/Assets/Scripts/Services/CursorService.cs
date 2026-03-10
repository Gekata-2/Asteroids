using UnityEngine;

namespace Services
{
    public class CursorService
    {
        public void SetCursorVisibility(bool visible)
        {
            Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = visible;
        }
    }
}