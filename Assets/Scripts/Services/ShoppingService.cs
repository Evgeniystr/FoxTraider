using Aestar.Data;
using System;
using Zenject;

namespace Aestar.Services
{
    public class ShoppingService
    {
        public event Action<ShopStockConfig> OnOpenShop;
        public event Action OnCloseShop;

        [Inject]
        private PlayerInventoryService _playerAvatarService;
        private ShopStockConfig _openedShopConfig;


        public void OpenShop(ShopStockConfig stockConfig)
        {
            if(_openedShopConfig == null)
            {
                _openedShopConfig = stockConfig;
                OnOpenShop?.Invoke(stockConfig);
            }
        }

        public void CloseShop()
        {
            _openedShopConfig = null;
            OnCloseShop?.Invoke();
        }

        public void Buy(EGoods goodsID, int price)
        {
            _playerAvatarService.EarnGoods(goodsID);
            _playerAvatarService.SpendMoney(price);
        }

        public void Sell(EGoods goodsID, int price)
        {
            _playerAvatarService.SpendGoods(goodsID);
            _playerAvatarService.EarnMoney(price);
        }
    }
}