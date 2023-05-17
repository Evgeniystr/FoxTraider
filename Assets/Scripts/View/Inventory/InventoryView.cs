using Aestar.Data;
using Aestar.Pool;
using Aestar.Services;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Aestar.View
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _viewport;
        [SerializeField]
        private Transform _itemsParent;
        [SerializeField]
        private InventoryItemView _itemPrefab;
        [SerializeField]
        private Button _openButton;
        [SerializeField]
        private Button _closeButton;

        private InventoryItemPool _inventoryItemsPool;
        private List<InventoryItemView> _allItems;

        [Inject]
        private InputService _inputService;
        [Inject]
        private PlayerInventoryService _inventoryService;
        [Inject]
        private GoodsConfig _goodsConfig;

        private void Awake()
        {
            _openButton.onClick.AddListener(OpenInventory);
            _closeButton.onClick.AddListener(CloseInventory);

            _inventoryItemsPool = new InventoryItemPool(_itemPrefab, _itemsParent);
            _allItems = new List<InventoryItemView>();

            InitialCleanup();
            CloseInventory();
        }

        private void OnDestroy()
        {
            _openButton.onClick.RemoveAllListeners();
            _closeButton.onClick.RemoveAllListeners();
        }

        private void OpenInventory()
        {
            _inputService.SetMoveInputBlock(true);

            var goods = _inventoryService.Inventory.Goods;

            foreach (var keyValuePair in goods)
            {
                var goodID = keyValuePair.Key;
                var goodQuantity = keyValuePair.Value;

                var sprite = _goodsConfig.GetItem(goodID).Icon;

                var itemView = _inventoryItemsPool.Get();
                itemView.Setup(sprite, goodQuantity);

                _allItems.Add(itemView);
            }

            _viewport.SetActive(true);
        }

        private void CloseInventory()
        {
            _inputService.SetMoveInputBlock(false);

            foreach (var item in _allItems)
                _inventoryItemsPool.ReleaseItem(item);

            _allItems.Clear();

            _viewport.SetActive(false);
        }

        private void InitialCleanup()
        {
            for (int i = 0; i < _itemsParent.childCount; i++)
            {
                var childToDestroy = _itemsParent.GetChild(i).gameObject;
                Destroy(childToDestroy);
            }
        }
    }
}