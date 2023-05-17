using Aestar.Data;
using Aestar.Pool;
using Aestar.Services;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Aestar.View
{
    public class TradingWindowView : MonoBehaviour
    {
        [SerializeField]
        private ShopItemView _shopItemPrefab;
        [SerializeField]
        private Transform _shopItemsParent;
        [SerializeField]
        private GameObject _viewport;
        [SerializeField]
        private Button _closeButton;

        [Inject]
        private InputService _inputService;
        [Inject]
        private GoodsConfig _goodsConfig;
        [Inject]
        private PlayerInventoryService _inventoryService;
        [Inject]
        private ShoppingService _shoppingService;

        private ShopItemPool _shopItemPool;
        private List<ShopItemView> _allItemsView;

        private void Awake()
        {
            _shopItemPool = new ShopItemPool(_shopItemPrefab, _shopItemsParent);
            _allItemsView = new List<ShopItemView>();

            _shoppingService.OnOpenShop += ShowWindow;
            _shoppingService.OnCloseShop += HideWindow;

            _closeButton.onClick.AddListener(_shoppingService.CloseShop);

            HideWindow();
            InitialCleanup();
        }

        private void OnDestroy()
        {
            _shoppingService.OnOpenShop -= ShowWindow;
            _shoppingService.OnCloseShop -= HideWindow;

            _closeButton.onClick.RemoveListener(HideWindow);
        }

        private void ShowWindow(ShopStockConfig shopStockConfig)
        {
            _inputService.SetMoveInputBlock(true);

            foreach (var stockItem in shopStockConfig.Stock)
            {
                var stockItemInstanceView = _shopItemPool.Get();
                var icon = _goodsConfig.GetItem(stockItem.GoodID).Icon;

                stockItemInstanceView.Setup(stockItem, icon);
                stockItemInstanceView.CheckAvaliability(_inventoryService.Inventory);

                _inventoryService.OnInventoryUpdated += stockItemInstanceView.CheckAvaliability;

                stockItemInstanceView.OnBuyClick += _shoppingService.Buy;
                stockItemInstanceView.OnSellClick += _shoppingService.Sell;

                _allItemsView.Add(stockItemInstanceView);
            }

            _viewport.SetActive(true);
        }

        private void HideWindow()
        {
            _inputService.SetMoveInputBlock(false);
            _viewport.SetActive(false);

            foreach (var item in _allItemsView)
            {
                _inventoryService.OnInventoryUpdated -= item.CheckAvaliability;
                item.OnBuyClick -= _shoppingService.Buy;
                item.OnSellClick -= _shoppingService.Sell;

                item.Cleanup();
                _shopItemPool.ReleaseItem(item);
            }

            _allItemsView.Clear();
        }

        private void InitialCleanup()
        {
            for (int i = 0; i < _shopItemsParent.childCount; i++)
            {
                var childToDestroy = _shopItemsParent.GetChild(i).gameObject;
                Destroy(childToDestroy);
            }
        }
    }
}