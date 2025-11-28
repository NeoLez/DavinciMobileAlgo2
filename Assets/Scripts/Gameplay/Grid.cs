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
            if (GetTower(pos) is null) return false;
            positions[pos] = tower;
            return true;
        }

        public bool RemoveTower(Vector2 pos) {
            if (GetTower(pos) is null) return false;
            positions.Remove(pos);
            return true;
        }
    }
}