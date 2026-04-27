using _Project.Scripts.Entities.UFO;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class UfosInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UfoFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<UfosController>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<UfosSpawner>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }
    }
}