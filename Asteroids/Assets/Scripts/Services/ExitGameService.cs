#if UNITY_EDITOR
using UnityEngine;
#endif


namespace Services
{
    public class ExitGameService
    {
        public void PerformExit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}