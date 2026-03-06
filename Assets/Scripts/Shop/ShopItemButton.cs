using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Root.Shop
{
    public class ShopItemButton : MonoBehaviour
    {
        [SerializeField] private TextTranslate itemName;
        [SerializeField] private TextMeshProUGUI itemCost;
        [SerializeField] private TextMeshProUGUI itemRarity;
        [SerializeField] private Image itemImage;
        [SerializeField] private Button myButton;
        private ShopItemSO itemData;

        public void SetItem(ShopItemSO itemData)
        {
            this.itemData = itemData;

            // Yo: Asigno el ID para que el componente TextTranslate lo traduzca.
            if (itemName != null) itemName.SetId(itemData.itemName);

            // Yo: Cargo el costo e imagen desde el SO.
            if (itemCost != null) itemCost.text = itemData.cost.ToString();
            if (itemImage != null) itemImage.sprite = itemData.icon();

            // Yo: Configuro el click del boton.
            myButton.onClick.RemoveAllListeners();
            myButton.onClick.AddListener(Buy);
        }

        private void Buy()
        {
            // Yo: Intento comprar. Si el SO dice que se pudo, destruyo el boton.
            if (itemData != null && itemData.BuyItem())
            {
                Destroy(gameObject);
            }
        }
    }
}