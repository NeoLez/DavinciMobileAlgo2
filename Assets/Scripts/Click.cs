using System;
using System.Collections.Generic;
using Root.Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Grid = Root.Gameplay.Grid;

namespace Root {
    public class Click : MonoBehaviour {
        private TowerSO TowerToPlace;
        private void Start() {
            EventManager.Subscribe<EventPayloads.MenuSelectedTowerSO>(HandleTowerSelected);

            InputSystem.Instance.OnTap += Tap;
        }

        private void HandleTowerSelected(EventPayloads.MenuSelectedTowerSO ctx)
        {
            TowerToPlace = ctx.TowerSO;
        }

        private void Tap(Vector2 touch)
        {
            if (!IsScreenPositionOverUI(touch)) {
                if(!Physics.Raycast(Camera.main.ScreenPointToRay(touch), out var hit)) return;
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

        private void OnDestroy()
        {
            InputSystem.Instance.OnTap -= Tap;
            EventManager.Unsubscribe<EventPayloads.MenuSelectedTowerSO>(HandleTowerSelected);
        }

        public GraphicRaycaster uiRaycaster;
        public EventSystem eventSystem;

        public bool IsScreenPositionOverUI(Vector2 screenPos)
        {
            PointerEventData eventData = new PointerEventData(eventSystem);
            eventData.position = screenPos;

            List<RaycastResult> results = new List<RaycastResult>();
            uiRaycaster.Raycast(eventData, results);
            return results.Count > 0;
        }
    }
}