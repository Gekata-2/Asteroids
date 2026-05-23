using _Project.Scripts.Entities.UFO;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class UfosInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindFactory<Object, Ufo, UfoPrefabFactory>().FromFactory<PrefabFactory<Ufo>>();
            Container.BindInterfacesAndSelfTo<UfoFactory>().AsSingle();
            Container.BindInterfacesTo<UfosController>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<UfosSpawner>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }
    }
}