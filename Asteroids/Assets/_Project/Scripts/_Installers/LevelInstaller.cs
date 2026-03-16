using _Project.Scripts.Entities;
using _Project.Scripts.LevelBounds;
using Zenject;

namespace _Project.Scripts._Installers
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EntitiesContainer>().FromNew().AsSingle().NonLazy();
            Container.Bind<LevelBounds.LevelBounds>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<IPositionWrapper>().To<InvertedClampedEntityPositionWrapper>().FromNew().AsSingle()
                .NonLazy();
            Container.BindInterfacesAndSelfTo<EntityOutOfBoundsController>().FromNew().AsSingle().NonLazy();
        }
    }
}