using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace _Project.Scripts.Sfx
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Audio Registry", fileName = "Audio Registry", order = 0)]
    public class AudioRegistry : ScriptableObject
    {
        [field: SerializeField] public SerializedDictionary<SFX, AudioData> Data { get; private set; }
    }
}