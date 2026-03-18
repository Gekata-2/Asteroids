using System.Collections;
using _Project.Scripts.Services.SceneManagement;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private Scenes startingScene;
        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Start()
        {
            StartCoroutine(StartGameRoutine());
        }

        private IEnumerator StartGameRoutine()
        {
            yield return null;
            _sceneLoader.LoadScene(startingScene);
        }
    }
}