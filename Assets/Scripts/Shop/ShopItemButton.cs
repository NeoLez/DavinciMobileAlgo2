using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Root.Shop {
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
            itemName.SetId(itemData.itemName);
            
            itemCost.text = itemData.cost.ToString();
            itemImage.sprite = itemData.icon();

            myButton.onClick.AddListener(Buy);
        }

        private void Buy()
        {
            Debug.Log("Click");
            if(itemData.BuyItem()) 
                Destroy(gameObject);
        }
    }
}