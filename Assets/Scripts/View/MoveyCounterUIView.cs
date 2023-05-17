using Aestar.Data;
using Aestar.Services;
using TMPro;
using UnityEngine;
using Zenject;

public class MoveyCounterUIView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _moneyValue;

    [Inject]
    private PlayerInventoryService _inventoryService;


    void Start()
    {
        _inventoryService.OnInventoryUpdated += UpdateMoneyValue;
        UpdateMoneyValue(_inventoryService.Inventory);
    }

    private void UpdateMoneyValue(Inventory inventory)
    {
        _moneyValue.text = inventory.Money.ToString();
    }
}
