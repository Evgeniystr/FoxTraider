using Aestar.Data;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Aestar.Data.ShopStockConfig;

namespace Aestar.View
{
    public class ShopItemView : MonoBehaviour
    {
        public event Action<EGoods, int> OnBuyClick;
        public event Action<EGoods, int> OnSellClick;

        [SerializeField]
        private GameObject _viewport;
        [SerializeField]
        private Image _icon;
        [Space]
        [SerializeField]
        private GameObject _buyOperationRoot;
        [SerializeField]
        private Button _buyButton;
        [SerializeField]
        private TMP_Text _buyPriceValue;
        [Space]
        [SerializeField]
        private GameObject _sellOperationRoot;
        [SerializeField]
        private Button _sellButton;
        [SerializeField]
        private TMP_Text _sellPriceValue;

        private StockConfigEntry _stockItemData;

        public void Setup(StockConfigEntry stockConfigEntry, Sprite itemSprite)
        {
            _stockItemData = stockConfigEntry;

            _icon.sprite = itemSprite;

            _buyOperationRoot.SetActive(_stockItemData.IsPurchasable);
            if (_stockItemData.IsPurchasable)
            {
                _buyPriceValue.text = _stockItemData.BuyPrice.ToString();
                _buyButton.onClick.AddListener(() => OnBuyClick.Invoke(_stockItemData.GoodID, _stockItemData.BuyPrice));
            }

            _sellOperationRoot.SetActive(_stockItemData.IsSellable);
            if (_stockItemData.IsSellable)
            {
                _sellPriceValue.text = _stockItemData.SellPrice.ToString();
                _sellButton.onClick.AddListener(() => OnSellClick.Invoke(_stockItemData.GoodID, _stockItemData.SellPrice));
            }

            SetViewActive(true);
        }

        public void CheckAvaliability(Inventory playerInventory)
        {
            if (_stockItemData.IsPurchasable)
            {
                _buyButton.interactable = _stockItemData.BuyPrice <= playerInventory.Money;
            }

            if (_stockItemData.IsSellable)
            {
                _sellButton.interactable = playerInventory.HasGoods(_stockItemData.GoodID);
            }
        }

        public void SetViewActive(bool state)
        {
            _viewport.SetActive(state);
        }

        public void Cleanup()
        {
            _buyButton.onClick.RemoveAllListeners();
            _sellButton.onClick.RemoveAllListeners();
            SetViewActive(false);
        }
    }
}