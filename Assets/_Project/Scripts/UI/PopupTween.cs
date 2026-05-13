using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class PopupTween : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private float _startingScale;
        [SerializeField] private Ease _ease;

        private void OnDestroy()
        {
            DOTween.Kill(this);
        }

        private void OnEnable()
        {
            DOTween.Kill(this);
            transform
                .DOScale(Vector3.one, _duration)
                .From(Vector3.one * _startingScale)
                .SetEase(_ease)
                .SetId(this);
        }
    }
}