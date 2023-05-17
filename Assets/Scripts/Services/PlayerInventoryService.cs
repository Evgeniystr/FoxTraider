using Aestar.Data;
using System;

namespace Aestar.Services
{ 
    public class PlayerInventoryService
    {
        public event Action<Inventory> OnInventoryUpdated;

        public Inventory Inventory { get; private set; }
        public bool IsMovementBlocked { get; private set; }

        public PlayerInventoryService() 
        {
            Inventory = new Inventory();
        }

        public void SpendMoney(int value)
        {
            Inventory.SpendMoney(value);
            OnInventoryUpdated?.Invoke(Inventory);
        }

        public void EarnMoney(int value)
        {
            Inventory.EarnMoney(value);
            OnInventoryUpdated?.Invoke(Inventory);
        }

        public void SpendGoods(EGoods ID, int count = 1)
        {
            Inventory.SpendGoods(ID, count);
            OnInventoryUpdated?.Invoke(Inventory);
        }

        public void EarnGoods(EGoods ID, int count = 1)
        {
            Inventory.EarnGoods(ID, count);
            OnInventoryUpdated?.Invoke(Inventory);
        }
    }
}
