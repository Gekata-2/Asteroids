using System;
using Newtonsoft.Json;

namespace _Project.Scripts.Services.DataPersistence
{
    [Serializable]
    public class SaveData
    {
        private bool _isAdsRemoved;

        public int Score;
        public float Time;
        public DateTime Date;

        public bool IsAdsRemoved
        {
            get => _isAdsRemoved;
            set
            {
                _isAdsRemoved = value;
                Date = DateTime.Now;
            }
        }

        public SaveData(int score, float time, DateTime date = default, bool isAdsRemoved = false)
        {
            Score = score;
            Time = time;
            Date = date;
            _isAdsRemoved = isAdsRemoved;
        }

        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
}