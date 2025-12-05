using Root.Shop;
using UnityEngine;

public class ShopItemButtonCreator : MonoBehaviour
{
    [SerializeField]
    private ShopItemSO[] _items;
    [SerializeField]
    private Transform _contentParent;
    [SerializeField]
    private ShopItemButton _buttonPrefab;

    private void Start()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i].CanBeBought())
            {
                ShopItemButton newItem = Instantiate(_buttonPrefab, _contentParent);
                newItem.SetItem(_items[i]);
            }
        }
    }
}
