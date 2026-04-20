using UnityEngine;

namespace _Project.Scripts.Services.Awards
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Score Config", fileName = "Score Config", order = 0)]
    public class ScoreConfig : ScriptableObject
    {
        [field: SerializeField] public AliveDurationScoreConfig AliveDurationScore { get; private set; }
        [field:SerializeField] public int StartingScore { get; private set; }
    }
}