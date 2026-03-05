using Player;
using UnityEngine;
using Zenject;

namespace _Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig playerConfig;

        public override void InstallBindings()
        {
            Container.Bind<LevelBounds>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<PlayerConfig>().FromScriptableObject(playerConfig).AsSingle().NonLazy();
            Container.Bind<PlayerInputActionMap>().FromNew().AsSingle().NonLazy();
            Container.Bind<IInput>().To<InputHandler>().FromNew().AsSingle().NonLazy();
            Container.Bind<EntitiesContainer>().FromNew().AsSingle().NonLazy();

            Container.Bind<IPositionWrapper>().To<InvertedClampedEntityPositionWrapper>().FromNew().AsSingle()
                .NonLazy();
            Container.BindInterfacesAndSelfTo<EntityOutOfBoundsController>().FromNew().AsSingle().NonLazy();
        }
    }
}