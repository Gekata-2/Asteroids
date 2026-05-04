using System;
using Newtonsoft.Json;

namespace _Project.Scripts.DataPersistence
{
    [Serializable]
    public class SaveData
    {
        public int Score;
        public float Time;
        public bool IsAdsRemoved;

        public SaveData(int score, float time, bool isAdsRemoved = false)
        {
            Score = score;
            Time = time;
            IsAdsRemoved = isAdsRemoved;
        }

        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
}