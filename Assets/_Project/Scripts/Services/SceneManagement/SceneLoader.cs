using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Services.SceneManagement
{
    public class SceneLoader
    {
        private readonly Dictionary<Scenes, string> _scenesNames = new() { { Scenes.Level, "Level" } };
        
        private void LoadScene(Scenes scene)
        {
            string sceneName = GetScene(scene);
            if (sceneName != null)
                SceneManager.LoadScene(sceneName);
            else
                Debug.LogException(new ArgumentException($"No scene by key \"{scene}\" exists"));
        }

        private string GetScene(Scenes scene) =>
            _scenesNames.TryGetValue(scene, out string sceneName) ? sceneName : null;

        public void LoadLevelScene()
            => LoadScene(Scenes.Level);
    }
}