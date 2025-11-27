using UnityEngine;

public class ShopItemButtonCreator : MonoBehaviour
{
    [SerializeField]
    private ItemSO[] _items;
    [SerializeField]
    private Transform _contentParent;
    [SerializeField]
    private ItemButton _buttonPrefab;

    private void Start()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            ItemButton newItem = Instantiate(_buttonPrefab, _contentParent);
            newItem.SetItem(_items[i]);
        }
    }
}
