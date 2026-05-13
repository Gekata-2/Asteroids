using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Meta.IAP.UI
{
    public class ProductCardView : MonoBehaviour
    {
        public event Action<ProductCardView> BuyClicked;

        [SerializeField] private Button _buyButton;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private TMP_Text _iapName;
        [SerializeField] private GameObject _loading;

        [SerializeField] private IAP _iap;

        public IAP IAP => _iap;

        private void Start()
        {
            _buyButton.onClick.AddListener(OnBuyClicked);
            SetIsProcessing(false);
        }

        private void OnDestroy()
        {
            _buyButton.onClick.RemoveListener(OnBuyClicked);
        }

        public void Initialize(IAP iap, IAPData data)
        {
            _iap = iap;
            _iapName.text = data.DisplayName;
            _priceText.text = $"{data.Price} $";
        }

        private void OnBuyClicked()
        {
            BuyClicked?.Invoke(this);
        }

        public void SetAvailable(bool isAvailable)
            => _buyButton.interactable = isAvailable;

        public void SetIsProcessing(bool isProcessing)
            => _loading.SetActive(isProcessing);
    }
}