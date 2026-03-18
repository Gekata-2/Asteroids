using _Project.Scripts.Services.SceneManagement;
using UnityEngine;
using Zenject;

namespace _Project.Scripts._Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private ScenesData scenesData;
        
        public override void InstallBindings()
        {
            Container.Bind<ScenesData>().FromScriptableObject(scenesData).AsSingle();
            Container.Bind<SceneLoader>().FromNew().AsSingle();
        }
    }
}