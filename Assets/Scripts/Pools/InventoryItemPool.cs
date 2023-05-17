using Aestar.View;
using UnityEngine;

namespace Aestar.Pool
{
    public class InventoryItemPool : APool<InventoryItemView>
    {
        private InventoryItemView _prefab;
        private Transform _parent;

        public InventoryItemPool(InventoryItemView prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
        }

        protected override InventoryItemView CreateItem()
        {
            var instance = GameObject.Instantiate(_prefab, _parent);
            instance.SetViewActive(false);
            return instance;
        }
    }
}