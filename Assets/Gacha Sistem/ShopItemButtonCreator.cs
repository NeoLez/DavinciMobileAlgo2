using Root.Shop;
using Root.Gameplay; 
using UnityEngine;

namespace Root.Shop
{ // Te recomiendo meterlo en el namespace para evitar lios
    public class ShopItemButtonCreator : MonoBehaviour
    {
        [SerializeField] private ShopItemSO[] _items;
        [SerializeField] private Transform _contentParent;
        [SerializeField] private ShopItemButton _buttonPrefab;

        private void Start()
        {
            
            EventManager.Subscribe<EventPayloads.EnemiesEliminated>(OnDataReset);

            CrearBotones();
        }

        private void OnDestroy()
        {
            EventManager.Unsubscribe<EventPayloads.EnemiesEliminated>(OnDataReset);
        }

        private void OnDataReset(EventPayloads.EnemiesEliminated e)
        {
            foreach (Transform child in _contentParent)
            {
                Destroy(child.gameObject);
            }

            CrearBotones();
        }

        private void CrearBotones()
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
}