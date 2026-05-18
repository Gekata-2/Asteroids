using System;
using UnityEngine;

namespace _Project.Scripts.Meta.IAP
{
    [Serializable]
    public class IAPData
    {
        public string DisplayName;
        public string StoreName;
        public Sprite Icon;
        public float Price;
        public IAPType Type;
    }
}