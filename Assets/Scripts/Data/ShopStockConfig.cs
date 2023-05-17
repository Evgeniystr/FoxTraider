using System;
using UnityEngine;

namespace Aestar.Data
{
    [CreateAssetMenu(fileName = "ShopStockConfig", menuName = "AestarConfigs/ShopStockConfig", order = 1)]

    public class ShopStockConfig : ScriptableObject
    {
        public StockConfigEntry[] Stock;


        [Serializable]
        public class StockConfigEntry
        {
            public EGoods GoodID;

            public bool IsPurchasable;
            public int BuyPrice;

            public bool IsSellable;
            public int SellPrice;
        }
    }
}