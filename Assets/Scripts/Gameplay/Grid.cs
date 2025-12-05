using System.Collections.Generic;
using UnityEngine;

namespace Root.Gameplay {
    public class Grid : MonoBehaviour {
        public static Grid Ins;
        private void Awake() {
            Ins = this;

            foreach (var blockedPosition  in blockedPositions_Editor) {
                blockedPositions.Add(blockedPosition);
            }
        }

        [SerializeField] private List<Vector2> blockedPositions_Editor;
        private HashSet<Vector2> blockedPositions = new ();
        private Dictionary<Vector2, Tower> positions = new();

        public bool IsPositionBlocked(Vector2 pos) {
            return blockedPositions.Contains(pos);
        }

        public Tower GetTower(Vector2 pos) {
            positions.TryGetValue(pos, out Tower value);
            return value;
        }

        public void SetTower(Vector2 pos, Tower tower) {
            tower.transform.position = pos;
            positions[pos] = tower;
        }

        public bool RemoveTower(Vector2 pos) {
            Tower tower = GetTower(pos);
            if (tower is null) return false;
            positions.Remove(pos);
            Destroy(tower.gameObject);
            return true;
        }

        public bool BuyTower(Vector2 pos, TowerSO towerSO) {
            if (GetTower(pos) != null) return false;
            if (blockedPositions.Contains(pos)) return false;
            if (!Level.Ins.gold.ConsumeGold(towerSO.levelCosts[0])) return false;
            
            GameObject tow = Instantiate(towerSO.levels[0]);
            Tower to = tow.GetComponent<Tower>();
            SetTower(pos, to);
            return true;
        }
        
        public bool Upgrade(Vector2 pos) {
            Tower tower = GetTower(pos);
            if (GetTower(pos) == null) return false;
            int targetLevel = tower.GetUpgradeLevel() + 1;
            TowerSO towerSO = tower.GetTowerSO();
            if (targetLevel == towerSO.levels.Count) return false;
            if (!Level.Ins.gold.ConsumeGold(towerSO.levelCosts[targetLevel])) return false;

            RemoveTower(pos);
            
            GameObject tow = Instantiate(towerSO.levels[targetLevel]);
            Tower to = tow.GetComponent<Tower>();
            SetTower(pos, to);
            return true;
        }
    }
}