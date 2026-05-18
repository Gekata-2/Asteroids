using UnityEngine;

namespace _Project.Scripts.Sfx
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicEmitter : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        
        public void SetData(SoundEmitterData data)
        {
            _source.Stop();
            _source.clip = data.AudioClip;
            _source.outputAudioMixerGroup = data.MixerGroup;
            _source.loop = data.Loop;
        }

        public void Play() 
            => _source.Play();

        public void Stop()
            => _source.Stop();
    }
}