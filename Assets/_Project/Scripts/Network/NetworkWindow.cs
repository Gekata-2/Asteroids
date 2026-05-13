using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Network
{
    public class NetworkWindow : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Color onlineColor;
        [SerializeField] private Color offlineColor;

        public void SetIsConnected(bool isConnected)
            => icon.color = isConnected ? onlineColor : offlineColor;
    }
}