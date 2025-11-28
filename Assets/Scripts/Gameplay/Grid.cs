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

        public bool SetTower(Vector2 pos, Tower tower) {
            if (blockedPositions.Contains(pos) || GetTower(pos) is not null) return false;
            tower.transform.position = pos;
            positions[pos] = tower;
            return true;
        }

        public bool RemoveTower(Vector2 pos) {
            Tower tower = GetTower(pos);
            if (tower is null) return false;
            positions.Remove(pos);
            Destroy(tower.gameObject);
            return true;
        }
    }
}