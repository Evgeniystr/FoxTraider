using System;
using System.Linq;
using UnityEngine;

namespace Aestar.Data
{
    [CreateAssetMenu(fileName = "GoodsConfig", menuName = "AestarConfigs/GoodsConfig", order = 0)]
    public class GoodsConfig : ScriptableObject
    {
        public GoodsEntry[] Goods;

        public GoodsEntry GetItem(EGoods ID)
        {
            return Goods.FirstOrDefault(i => i.ID == ID);
        }

        [Serializable]
        public class GoodsEntry
        {
            public EGoods ID;
            public Sprite Icon;
        }
    }
}