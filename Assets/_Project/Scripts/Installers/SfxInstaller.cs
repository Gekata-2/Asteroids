using _Project.Scripts.Sfx;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class SfxInstaller : MonoInstaller
    {
        [SerializeField] private SoundEmitter _prefab;
        [SerializeField] private MusicEmitter _musicEmitterPrefab;
        [SerializeField] private AudioRegistry _audioRegistry;
        
        public override void InstallBindings()
        {
            Container.BindFactory<SoundEmitter, SoundEmitterFactory>().FromComponentInNewPrefab(_prefab);
            Container.BindFactory<MusicEmitter,MusicEmitterFactory>().FromComponentInNewPrefab(_musicEmitterPrefab);
            Container.BindInterfacesAndSelfTo<AudioSystem>().AsSingle();
            Container.Bind<AudioRegistry>().FromScriptableObject(_audioRegistry).AsSingle();
        }
    }
}