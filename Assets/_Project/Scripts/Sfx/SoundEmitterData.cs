using UnityEngine;
using UnityEngine.Audio;

namespace _Project.Scripts.Sfx
{
    public struct SoundEmitterData
    {
        public AudioClip AudioClip { get; }
        public AudioMixerGroup MixerGroup { get; }
        public bool RandomPitch { get; }
        public bool Loop { get; }

        public SoundEmitterData(AudioClip audioClip, AudioMixerGroup mixerGroup, bool randomPitch, bool loop = false)
        {
            AudioClip = audioClip;
            MixerGroup = mixerGroup;
            RandomPitch = randomPitch;
            Loop = loop;
        }
    }
}