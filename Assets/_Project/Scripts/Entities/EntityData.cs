using UnityEngine;

namespace _Project.Scripts.Entities
{
    public class EntityData : ScriptableObject
    {
        [SerializeField] private int score;
      
        public int Score => score;
    }
}