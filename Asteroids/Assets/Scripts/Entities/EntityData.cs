using UnityEngine;

namespace Entities
{
    public class EntityData : ScriptableObject
    {
        [SerializeField] private int score;
      
        public int Score => score;
    }
}