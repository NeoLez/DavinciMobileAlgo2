using System.Collections.Generic;
using UnityEngine;

namespace Root.Gameplay {
    public class Level : MonoBehaviour {
        public static Level Ins;
        private void Awake() {
            Ins = this;

            for(int i = 1; i < enemyPath.Count; i++) {
                PathLength += Vector2.Distance(enemyPath[i-1].transform.position, enemyPath[i].transform.position);
            }
            
        }

        public float playerHealth;
        public List<Transform> enemyPath;
        public float PathLength { get; private set; }
        public Grid grid;
        public IngameGold gold;
    }
}