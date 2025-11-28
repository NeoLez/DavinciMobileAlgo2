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
                GameObject tow = Instantiate(TowerToPlace.levels[0]);
                Tower to = tow.GetComponent<Tower>();
                grid.SetTower(posGrid, to);
            }
            else {
                tower.GetUpgradeLevel();
                grid.RemoveTower(posGrid);
                GameObject tow = Instantiate(TowerToPlace.levels[(tower.GetUpgradeLevel() + 1) % TowerToPlace.levels.Count]);
                Tower to = tow.GetComponent<Tower>();
                grid.SetTower(posGrid, to);
            }
        }
    }
}