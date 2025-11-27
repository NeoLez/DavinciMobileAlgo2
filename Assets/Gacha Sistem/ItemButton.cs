using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class ItemButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _itemName;
    [SerializeField]
    private TextMeshProUGUI _itemCost;
    [SerializeField]
    private TextMeshProUGUI _itemRarity;
    [SerializeField]
    private int _price;
    [SerializeField]
    private Image _itemImage;
    [SerializeField]
    private Button _myButton;

    public void SetItem(ItemSO itemData)
    {
        _itemName.text = itemData.ItemName;
        _price = itemData.ItemCost;
        _itemCost.text = itemData.ItemCost.ToString();
        _itemRarity.text = itemData.Rarity.ToString();
        _itemImage.sprite = itemData.ItemIcon;

        _myButton.onClick.AddListener(itemData.BuyItem);
    }
}
