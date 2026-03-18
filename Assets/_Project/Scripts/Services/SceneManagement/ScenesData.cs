using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace _Project.Scripts.Services.SceneManagement
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Create Scenes Data", fileName = "Scenes Data", order = 0)]
    public class ScenesData : ScriptableObject
    {
        [field: SerializeField] public SerializedDictionary<Scenes, string> Scenes { get; private set; }

        public string GetScene(Scenes scene) 
            => Scenes.TryGetValue(scene, out string sceneName) ? sceneName : null;
    }
}