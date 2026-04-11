using _Project.Scripts.DataPersistence;
using _Project.Scripts.Services.SceneManagement;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services
{
    public class GameBootstrap : MonoBehaviour
    {
        private SceneLoader _sceneLoader;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(SceneLoader sceneLoader, ISaveLoadService saveLoadService)
        {
            _sceneLoader = sceneLoader;
            _saveLoadService = saveLoadService;
        }

        private async void Start()
        {
            // For test before save data needed
            await _saveLoadService.Load();
            _sceneLoader.LoadLevelScene();
        }
    }
}