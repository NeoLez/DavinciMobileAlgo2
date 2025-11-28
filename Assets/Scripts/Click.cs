using Root.Gameplay;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Grid = Root.Gameplay.Grid;

namespace Root {
    public class Click : MonoBehaviour {
        [SerializeField] private TowerSO TowerToPlace;
        private void Start() {
            InputSystem.Instance.OnTap += Process;
        }

        private void Process(InputSystem.CustomTouch touch) {
            if(!Physics.Raycast(Camera.main.ScreenPointToRay(touch.CurrentPosition), out var hit)) return;
            Vector2 posGrid = new Vector2(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y));
            Grid grid = Level.Ins.grid;
            Tower tower = grid.GetTower(posGrid);
            if (tower == null) {
                if (!Level.Ins.gold.ConsumeGold(TowerToPlace.levelCosts[0])) return;
                GameObject tow = Instantiate(TowerToPlace.levels[0]);
                Tower to = tow.GetComponent<Tower>();
                grid.SetTower(posGrid, to);
            }
            else {
                int level = tower.GetUpgradeLevel() + 1;
                if(level >= TowerToPlace.levels.Count) return;
                if (!Level.Ins.gold.ConsumeGold(TowerToPlace.levelCosts[level])) return;
                
                grid.RemoveTower(posGrid);
                GameObject tow = Instantiate(TowerToPlace.levels[level]);
                Tower to = tow.GetComponent<Tower>();
                grid.SetTower(posGrid, to);
            }
        }
    }
}