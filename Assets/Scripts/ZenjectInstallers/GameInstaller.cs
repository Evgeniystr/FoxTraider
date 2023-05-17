
using Aestar.Data;
using Aestar.Services;
using UnityEngine;
using Zenject;

namespace Aestar.DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private GoodsConfig _goodsConfig;
        [Space]
        [SerializeField]
        private InputService _inputService;

        public override void InstallBindings()
        {
            Container.Bind<GoodsConfig>().FromInstance(_goodsConfig).AsSingle();

            Container.Bind<InputService>().FromInstance(_inputService).AsSingle();
            Container.Bind<PlayerInventoryService>().AsSingle();
            Container.Bind<ShoppingService>().AsSingle();
        }
    }
}

