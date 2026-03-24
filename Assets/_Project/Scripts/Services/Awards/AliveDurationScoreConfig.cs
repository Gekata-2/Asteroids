using System;
using UnityEngine;

namespace _Project.Scripts.Services.Awards
{
    [Serializable]
    public class AliveDurationScoreConfig
    {
        [field: SerializeField] public float TimeInterval { get; private set; } = 1f;
        [field: SerializeField] public int ScoreValue { get; private set; } = 1;
    }
}