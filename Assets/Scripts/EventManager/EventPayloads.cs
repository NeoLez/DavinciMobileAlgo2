using Root.Gameplay;
using UnityEngine;

namespace Root {
    public static class EventPayloads {
        public class BattleEndEvent {
            public bool Victory { get; private set; }
            public BattleEndEvent(bool victory) => Victory = victory;
        }

        public class EnemyReachedEnd {
            public int Damage { get; private set; }
            public EnemyReachedEnd(int damage) => Damage = damage;
        }

        public class EnemyDied {
            
        }

        public class EnemiesEliminated {
            
        }

        public class MenuSelectedTowerSO {
            public TowerSO TowerSO;

            public MenuSelectedTowerSO(TowerSO towerSO) {
                this.TowerSO = towerSO;
            }
        }

        public class GridSelectedTower {
            public Vector2 gridPosition;
            public Tower tower;

            public GridSelectedTower(Vector2 gridPosition, Tower tower) {
                this.gridPosition = gridPosition;
                this.tower = tower;
            }
        }

        public class GridDeselectedTower {
            
        }
    }
}