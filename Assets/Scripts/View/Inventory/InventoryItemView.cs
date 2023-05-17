using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Aestar.View
{
    public class InventoryItemView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _viewport;
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TMP_Text _quantity;


        public void Setup(Sprite icon, int quantity)
        {
            _icon.sprite = icon;
            _quantity.text = quantity.ToString();

            SetViewActive(true);
        }

        public void SetViewActive(bool state)
        {
            _viewport.SetActive(state);
        }
    }
}