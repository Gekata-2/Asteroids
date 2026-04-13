using _Project.Scripts.Analytics;
using _Project.Scripts.DataPersistence;
using _Project.Scripts.Services.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services
{
    public class GameBootstrap : MonoBehaviour
    {
        private SceneLoader _sceneLoader;
        private ISaveLoadService _saveLoadService;
        private IAnalytics _analytics;


        [Inject]
        private void Construct(SceneLoader sceneLoader, ISaveLoadService saveLoadService, IAnalytics analytics)
        {
            _sceneLoader = sceneLoader;
            _saveLoadService = saveLoadService;
            _analytics = analytics;
        }

        private void Start()
        {
            BootGame().Forget();
        }

        private async UniTask BootGame()
        {
            await UniTask.WhenAll(_saveLoadService.Load(), _analytics.Initialize());
            _sceneLoader.LoadLevelScene();
        }
    }
}