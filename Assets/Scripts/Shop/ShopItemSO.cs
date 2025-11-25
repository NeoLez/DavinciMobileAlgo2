using UnityEngine;

namespace Root.Shop {
    [CreateAssetMenu(menuName = "SO/ShopItem")]
    public class ShopItemSO : ScriptableObject {
        [SerializeField] private int _cost;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _itemName;
        [SerializeReference, SubclassSelector] private IShopBuyBehaviour buyBehaviour;
        [SerializeField] private ShopItemStatus _status;

        public int cost => _cost;
        public Sprite icon => _icon;
        public string itemName => _itemName;
        public ShopItemStatus status {
            get => _status;
            set => _status = value;
        }

        public void BuyItem() {
            Debug.Log($"Se compro el item {_itemName}");
            buyBehaviour.GiveItem();
        }

        public enum ShopItemStatus {
            Available,
            Locked,
            Purchased,
        }
    }
}