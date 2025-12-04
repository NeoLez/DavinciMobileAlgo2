using UnityEngine;

namespace Root.Shop {
    public abstract class  ShopItemSO : ScriptableObject {
        [SerializeField] private int _cost;
        [SerializeField] private string _itemName;
        [SerializeField] private ShopItemStatus _status;

        public int cost => _cost;
        public abstract Sprite icon();
        public string itemName => _itemName;
        public ShopItemStatus status {
            get => _status;
            set => _status = value;
        }

        public bool BuyItem() {
            if(!Database.Database.Ins.currencySystem.ConsumeCurrency(_cost)) return false;
            
            Debug.Log($"Se compro el item {itemName}"); 
            GiveItem();
            return true;
        }

        public abstract void GiveItem();

        public enum ShopItemStatus {
            Available,
            Locked,
            Purchased,
        }
    }
}