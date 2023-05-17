using Aestar.Data;
using Aestar.Services;
using UnityEngine;
using Zenject;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private ShopStockConfig _stockConfig;

    [Inject]
    private ShoppingService _shoppingService;


    public void OpenShop()
    {
        _shoppingService.OpenShop(_stockConfig);
    }

    public void CloseShop()
    {
        _shoppingService.CloseShop();
    }
}
