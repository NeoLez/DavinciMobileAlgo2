using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Root.Shop {
    public class ShopItemButton : MonoBehaviour {
        [SerializeField]
        private TextMeshProUGUI itemName;
        [SerializeField]
        private TextMeshProUGUI itemCost;
        [SerializeField]
        private TextMeshProUGUI itemRarity;
        [SerializeField]
        private int price;
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private Button myButton;

        public void SetItem(ShopItemSO itemData)
        {
            itemName.text = itemData.name;
            price = itemData.cost;
            itemCost.text = itemData.cost.ToString();
            itemImage.sprite = itemData.icon;

            myButton.onClick.AddListener(itemData.BuyItem);
        }
    }
}