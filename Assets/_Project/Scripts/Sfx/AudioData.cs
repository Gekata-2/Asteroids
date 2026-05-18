using System;
using UnityEngine;
using UnityEngine.Audio;

namespace _Project.Scripts.Sfx
{
    [Serializable]
    public class AudioData
    {
        [field: SerializeField] public AudioMixerGroup MixerGroup { get; private set; }
        [field: SerializeField] public bool RandomPitch { get; private set; }
    }
}