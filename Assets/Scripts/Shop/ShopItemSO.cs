using UnityEngine;

namespace Root.Shop {
    [CreateAssetMenu(menuName = "SO/ShopItem")]
    public class ShopItemSO : ScriptableObject {
        [SerializeField] private int _cost;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _localizationKey;
        [SerializeReference, SubclassSelector] private IShopBuyBehaviour buyBehaviour;
        [SerializeField] private ShopItemStatus _status;

        public int cost => _cost;
        public Sprite icon => _icon;
        public string itemName => Localization.Ins.GetTranslate(_localizationKey);
        public ShopItemStatus status {
            get => _status;
            set => _status = value;
        }

        public void BuyItem() {
            Debug.Log($"Se compro el item {itemName}");
            buyBehaviour.GiveItem();
        }

        public enum ShopItemStatus {
            Available,
            Locked,
            Purchased,
        }
    }
}