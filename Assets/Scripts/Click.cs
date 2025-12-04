using Root.Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using Grid = Root.Gameplay.Grid;

namespace Root {
    public class Click : MonoBehaviour {
        private TowerSO TowerToPlace;
        private void Start() {
            EventManager.Subscribe<EventPayloads.MenuSelectedTowerSO>(ctx => {
                TowerToPlace = ctx.TowerSO;
            });
        }

        private void Update() {
            if (!EventSystem.current.IsPointerOverGameObject()) {
                if (Input.GetMouseButtonDown(0)) {
                    if(!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit)) return;
                    Vector2 posGrid = new Vector2(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y));
                    Tower tower = Level.Ins.grid.GetTower(posGrid);
                    if (tower != null) {
                        EventManager.Trigger(new EventPayloads.GridSelectedTower(posGrid, tower));
                    }
                    else {
                        if (TowerToPlace != null) {
                            Grid.Ins.BuyTower(posGrid, TowerToPlace);
                        }
                    }
                }
            }
        }
    }
}