using _Project.Scripts.Services.SceneManagement;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services
{
    public class GameBootstrap : MonoBehaviour
    {
        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Start()
        {
            _sceneLoader.LoadLevelScene();
        }
    }
}