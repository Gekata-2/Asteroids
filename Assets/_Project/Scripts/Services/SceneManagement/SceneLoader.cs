using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Services.SceneManagement
{
    public class SceneLoader 
    {
        private readonly ScenesData _scenesData;

        public SceneLoader(ScenesData scenesData)
        {
            _scenesData = scenesData;
        }
        
        public void LoadScene(Scenes scene)
        {
            string sceneName = _scenesData.GetScene(scene);
            if (sceneName != null)
                SceneManager.LoadScene(sceneName);
            else
                Debug.LogException(new ArgumentException($"No scene by key \"{scene}\" exists"));
        }

        public void ReloadCurrentScene()
            => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}