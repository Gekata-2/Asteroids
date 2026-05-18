using _Project.Scripts.ObjectPools;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Sfx
{
    public class PoolsConfigsInstaller : MonoInstaller
    {
        [SerializeField] private PoolsConfigs _poolsConfigs;

        public override void InstallBindings()
        {
            Container.Bind<PoolsConfigs>().FromScriptableObject(_poolsConfigs).AsSingle();
        }
    }
}