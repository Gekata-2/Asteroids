using LevelBounds;
using Zenject;

namespace _Installers
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LevelBounds.LevelBounds>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<IPositionWrapper>().To<InvertedClampedEntityPositionWrapper>().FromNew().AsSingle()
                .NonLazy();
            Container.BindInterfacesAndSelfTo<EntityOutOfBoundsController>().FromNew().AsSingle().NonLazy();
        }
    }
}