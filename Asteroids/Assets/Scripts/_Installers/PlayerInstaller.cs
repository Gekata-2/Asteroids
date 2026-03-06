using Entities;
using Player;
using UnityEngine;
using Zenject;

namespace _Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private PlayerWeaponsConfig weaponsConfig;

        public override void InstallBindings()
        {
            Container.Bind<PlayerConfig>().FromScriptableObject(playerConfig).AsSingle().NonLazy();
            Container.Bind<PlayerWeaponsConfig>().FromScriptableObject(weaponsConfig).AsSingle().NonLazy();
            Container.Bind<PlayerInputActionMap>().FromNew().AsSingle().NonLazy();
            Container.Bind<IInput>().To<InputHandler>().FromNew().AsSingle().NonLazy();
            Container.Bind<EntitiesContainer>().FromNew().AsSingle().NonLazy();
        }
    }
}