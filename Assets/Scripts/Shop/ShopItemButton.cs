using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Root.Shop
{
    public class ShopItemButton : MonoBehaviour
    {
        [SerializeField] private TextTranslate itemName;
        [SerializeField] private TextMeshProUGUI itemCost;
        [SerializeField] private Image itemImage;
        [SerializeField] private Button myButton;

        // La dejo publica para que el Creator la pueda asignar al instanciar
        [HideInInspector] public ConfirmationPopup errorPopup;
        private string noMoneyTranslationKey = "ID_NO_MONEY";
        private ShopItemSO itemData;

        public void SetItem(ShopItemSO itemData)
        {
            this.itemData = itemData;
            if (itemName != null) itemName.SetId(itemData.itemName);
            if (itemCost != null) itemCost.text = itemData.cost.ToString();
            if (itemImage != null) itemImage.sprite = itemData.icon();

            myButton.onClick.RemoveAllListeners();
            myButton.onClick.AddListener(Buy);
        }

        private void Buy()
        {
            if (itemData != null && itemData.BuyItem())
            {
                Destroy(gameObject);
            }
            else
            {
                // Si no hay plata, uso el popup que me paso el Creator
                if (errorPopup != null)
                {
                    errorPopup.ShowPopup(noMoneyTranslationKey, () => { });
                }
            }
        }
    }
}