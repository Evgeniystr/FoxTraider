using System;
using System.Collections.Generic;

namespace Aestar.Data
{
    public class Inventory
    {
        public int Money { get; private set; } = 1000; //TEST value
        public Dictionary<EGoods, int> Goods { get; private set; }


        public Inventory() 
        {
            Goods = new Dictionary<EGoods, int>();
        }


        public bool HasGoods(EGoods ID)
        {
            var result = Goods.ContainsKey(ID) && Goods[ID] > 0;
            return result;
        }

        public void SpendMoney(int value)
        {
            if (value > Money)
                throw new Exception("[PlayerAvatarService] Can`t spend more money than have");

            Money -= value;
        }

        public void EarnMoney(int value)
        {
            Money += value;
        }

        public void SpendGoods(EGoods ID, int count = 1)
        {
            if (count > Goods[ID])
                throw new Exception($"[PlayerAvatarService] Can`t spend more goods than have: {ID}");

            Goods[ID] -= count;
        }

        public void EarnGoods(EGoods ID, int count = 1)
        {
            if (Goods.ContainsKey(ID))
                Goods[ID] += count;
            else
                Goods.Add(ID, count);
        }

    }
}