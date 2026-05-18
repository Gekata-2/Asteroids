using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.UI
{
    public class HoverableTween : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float _hoveredScale;
        [SerializeField] private Ease _pointerEnterEase;
        [SerializeField] private float _enterDuration;
        [SerializeField] private Ease _pointerExitEase;
        [SerializeField] private float _exitDuration;

        private void OnDestroy()
        {
            DOTween.Kill(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            DOTween.Kill(this);
            transform
                .DOScale(Vector3.one * _hoveredScale, _enterDuration)
                .SetEase(_pointerEnterEase)
                .SetId(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            DOTween.Kill(this);
            transform
                .DOScale(Vector3.one, _exitDuration)
                .SetEase(_pointerExitEase)
                .SetId(this);
        }
    }
}