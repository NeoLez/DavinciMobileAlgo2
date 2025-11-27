using System.Collections.Generic;
using UnityEngine;

namespace Root.Gameplay {
    public class Level : MonoBehaviour {
        public static Level Ins;
        private void Awake() {
            Ins = this;
        }

        public float playerHealth;
        public List<Transform> enemyPath;
    }
}