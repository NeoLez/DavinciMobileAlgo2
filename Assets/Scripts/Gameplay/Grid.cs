using System.Collections.Generic;
using UnityEngine;

namespace Root.Gameplay
{
    public class Grid : MonoBehaviour
    {
        public static Grid Ins;

        [Header("Configuracion de Bloqueo")]
        [Tooltip("Arrastra aca los GameObjects (Tiles) que queres bloquear")]
        [SerializeField] private List<GameObject> objetosParaBloquear;

        [SerializeField] private List<Vector2> blockedPositions_Editor;
        private HashSet<Vector2> blockedPositions = new();
        private Dictionary<Vector2, Tower> positions = new();

        private void Awake()
        {
            Ins = this;

            foreach (var blockedPosition in blockedPositions_Editor)
            {
                blockedPositions.Add(blockedPosition);
            }
        }

        // Metodo que se ejecuta cuando toco algo en el Inspector
        private void OnValidate()
        {
            if (objetosParaBloquear != null && objetosParaBloquear.Count > 0)
            {
                foreach (GameObject obj in objetosParaBloquear)
                {
                    if (obj == null) continue;

                    // Redondeo la posicion para que encaje perfecto en mi grilla
                    Vector2 pos = new Vector2(Mathf.Round(obj.transform.position.x), Mathf.Round(obj.transform.position.y));

                    // Si no la tenia guardada, la agrego a la lista real
                    if (!blockedPositions_Editor.Contains(pos))
                    {
                        blockedPositions_Editor.Add(pos);
                    }
                }
                // Limpio la lista de objetos para que el Inspector quede prolijo
                objetosParaBloquear.Clear();
            }
        }

        public bool IsPositionBlocked(Vector2 pos)
        {
            return blockedPositions.Contains(pos);
        }

        public Tower GetTower(Vector2 pos)
        {
            positions.TryGetValue(pos, out Tower value);
            return value;
        }

        public void SetTower(Vector2 pos, Tower tower)
        {
            tower.transform.position = pos;
            positions[pos] = tower;
        }

        public bool RemoveTower(Vector2 pos)
        {
            Tower tower = GetTower(pos);
            if (tower is null) return false;
            positions.Remove(pos);
            Destroy(tower.gameObject);
            return true;
        }

        public bool BuyTower(Vector2 pos, TowerSO towerSO)
        {
            if (GetTower(pos) != null) return false;
            if (blockedPositions.Contains(pos)) return false;
            if (!Level.Ins.gold.ConsumeGold(towerSO.levelCosts[0])) return false;

            GameObject tow = Instantiate(towerSO.levels[0]);
            Tower to = tow.GetComponent<Tower>();
            SetTower(pos, to);
            EventManager.Trigger(new EventPayloads.TowerBuilt());
            return true;
        }

        public bool Upgrade(Vector2 pos)
        {
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
            EventManager.Trigger(new EventPayloads.TowerUpgraded());
            return true;
        }
    }
}