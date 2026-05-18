using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Sfx
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEmitter : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private float _randomPitch = 0.05f;

        private CancellationTokenSource _cts;
        private AudioSystem _audioSystem;

        [Inject]
        private void Construct(AudioSystem audioSystem)
        {
            _audioSystem = audioSystem;
        }

        private void OnDestroy()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

        public void Initialize(SoundEmitterData data)
        {
            _source.clip = data.AudioClip;
            _source.outputAudioMixerGroup = data.MixerGroup;
            _source.loop = data.Loop;
            if (data.RandomPitch)
                _source.pitch = Random.Range(1 - _randomPitch, 1 + _randomPitch);
        }

        public void Play()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = new CancellationTokenSource();

            PlayRoutine(_cts.Token).Forget();
        }

        private async UniTask PlayRoutine(CancellationToken cancellationToken)
        {
            _source.Play();
            await UniTask.WaitUntil(() => !_source.isPlaying, cancellationToken: cancellationToken);
            _audioSystem.Release(this);
        }
    }
}