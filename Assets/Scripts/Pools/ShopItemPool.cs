using Aestar.View;
using UnityEngine;

namespace Aestar.Pool
{
    public class ShopItemPool : APool<ShopItemView>
    {
        private ShopItemView _prefab;
        private Transform _parent;

        public ShopItemPool(ShopItemView prefab, Transform parent)
        {
            _prefab = prefab; 
            _parent = parent;
        }


        protected override ShopItemView CreateItem()
        {
            var instance = GameObject.Instantiate(_prefab, _parent);
            instance.SetViewActive(false);
            return instance;
        }
    }
}