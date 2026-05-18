using System.Collections.Generic;
using _Project.Scripts.Extensions;
using _Project.Scripts.ObjectPools;
using _Project.Scripts.Services.AssetsManagement;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Sfx
{
    public class AudioSystem
    {
        private readonly SoundEmitterFactory _emitterFactory;
        private readonly MusicEmitterFactory _musicEmitterFactory;
        private readonly AudioRegistry _audioRegistry;
        private readonly IAssetProvider _assetProvider;

        private readonly ObjectPool<SoundEmitter> _pool;
        private readonly Dictionary<SFX, AudioClip> _audioClips = new();
        private readonly int _prewarmSize;
        private readonly Vector2 _inactiveEmitterPosition;
        
        private MusicEmitter _musicEmitter;

        public bool Initialized { get; private set; }
        
        public AudioSystem(PoolsConfigs poolsConfigs,
            SoundEmitterFactory emitterFactory,
            IAssetProvider assetProvider,
            AudioRegistry audioRegistry, MusicEmitterFactory musicEmitterFactory)
        {
            _inactiveEmitterPosition = poolsConfigs.InactiveObjectPosition;
            _prewarmSize = poolsConfigs.Sfx.PrewarmSize;
            _emitterFactory = emitterFactory;
            _assetProvider = assetProvider;
            _audioRegistry = audioRegistry;
            _musicEmitterFactory = musicEmitterFactory;
            _pool = new ObjectPool<SoundEmitter>(
                CreateEmitter, OnTakeEmitterFromPool, OnReturnEmitterToPool, OnDestroyEmitter,
                collectionCheck: true,
                defaultCapacity: poolsConfigs.Sfx.DefaultCapacity, maxSize: poolsConfigs.Sfx.MaxSize);
        }


        public void Initialize()
        {
            foreach (SFX sfx in _audioRegistry.Data.Keys)
            {
                _assetProvider.TryGetAsset(AssetsNames.GetName(sfx), out Object clip);
                _audioClips[sfx] = (AudioClip)clip;
            }

            _pool.PreWarm(_prewarmSize);
            _musicEmitter = _musicEmitterFactory.Create();
            Initialized = true;
        }

        private void OnDestroyEmitter(SoundEmitter emitter)
            => Object.Destroy(emitter.gameObject);

        private void OnReturnEmitterToPool(SoundEmitter emitter)
        {
            emitter.gameObject.SetActive(false);
            emitter.transform.position = _inactiveEmitterPosition;
        }

        private void OnTakeEmitterFromPool(SoundEmitter emitter)
            => emitter.gameObject.SetActive(true);

        private SoundEmitter CreateEmitter()
        {
            SoundEmitter emitter = _emitterFactory.Create();
            emitter.gameObject.SetActive(false);
            emitter.transform.position = _inactiveEmitterPosition;
            return emitter;
        }

        public void Release(SoundEmitter soundEmitter)
            => _pool.Release(soundEmitter);

        public void PlaySfx(SFX sound, Vector2 position)
        {
            AudioClip clip = _audioClips[sound];
            AudioData audioData = _audioRegistry.Data[sound];
            SoundEmitter soundEmitter = _pool.Get();
            soundEmitter.transform.position = position;
            soundEmitter.Initialize(new SoundEmitterData(clip, audioData.MixerGroup, audioData.RandomPitch));
            soundEmitter.Play();
        }

        public void PlayMusic(SFX music)
        {
            if (_musicEmitter == null)
                _musicEmitter = _musicEmitterFactory.Create();

            _musicEmitter.Stop();
            AudioClip clip = _audioClips[music];
            AudioData audioData = _audioRegistry.Data[music];
            _musicEmitter.SetData(new SoundEmitterData(clip, audioData.MixerGroup, false, true));
            _musicEmitter.Play();
        }
    }
}