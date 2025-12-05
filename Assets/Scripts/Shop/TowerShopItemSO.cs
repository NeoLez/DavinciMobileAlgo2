using UnityEngine;

namespace Root.Shop {
    [CreateAssetMenu(menuName = "SO/TowerShopItem")]
    public class TowerShopItemSO : ShopItemSO {
        [SerializeField] private TowerSO towerToUnlock;
        public override Sprite icon() {
            return towerToUnlock.icon;
        }

        public override void GiveItem() {
            Database.Database.Ins.towerDatabase.UnlockTower(towerToUnlock);
        }

        public override bool CanBeBought()
        {
            return !Database.Database.Ins.towerDatabase.IsTowerUnlocked(towerToUnlock);
        }
    }
}