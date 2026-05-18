using _Project.Scripts.Vfx;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class VfxInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindFactory<Object, VfxPlayable, VfxFactory>().FromFactory<PrefabFactory<VfxPlayable>>();
            Container.BindFactory<VfxPoolData, VfxPool, VfxPoolFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<VfxSystem>().AsSingle();
        }
    }
}