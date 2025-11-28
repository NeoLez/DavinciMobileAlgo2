using UnityEngine;

namespace Root.Shop {
    public abstract class  ShopItemSO : ScriptableObject {
        [SerializeField] private int _cost;
        [SerializeField] private string _localizationKey;
        [SerializeField] private ShopItemStatus _status;

        public int cost => _cost;
        public abstract Sprite icon();
        public string itemName => Localization.Ins.GetTranslate(_localizationKey);
        public ShopItemStatus status {
            get => _status;
            set => _status = value;
        }

        public void BuyItem() {
            Debug.Log($"Se compro el item {itemName}"); 
            GiveItem();
        }

        public abstract void GiveItem();

        public enum ShopItemStatus {
            Available,
            Locked,
            Purchased,
        }
    }
}