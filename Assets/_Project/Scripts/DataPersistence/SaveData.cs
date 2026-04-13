using System;
using Newtonsoft.Json;

namespace _Project.Scripts.DataPersistence
{
    [Serializable]
    public class SaveData
    {
        public int Score;
        public float Time;

        public SaveData(int score, float time)
        {
            Score = score;
            Time = time;
        }

        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
}