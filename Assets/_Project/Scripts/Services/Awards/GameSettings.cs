using System;
using UnityEngine;

namespace _Project.Scripts.Services.Awards
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Create Game Settings", fileName = "Game Settings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        [Serializable]
        public class AliveDurationScoreConfig
        {
            [field: SerializeField] public float TimeInterval { get; private set; } = 1f;
            [field: SerializeField] public int ScoreValue { get; private set; } = 1;
        }

        [field: SerializeField] public AliveDurationScoreConfig AliveDurationScore { get; private set; }
    }
}