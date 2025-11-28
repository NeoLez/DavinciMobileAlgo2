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
            EventManager.Subscribe<EventPayloads.EnemyReachedEnd>(TakeDamage);
            EventManager.Subscribe<EventPayloads.EnemiesEliminated>(OnEnemiesEliminated);
        }

        [SerializeField] private bool lost;
        private void TakeDamage(EventPayloads.EnemyReachedEnd payload) {
            playerHealth -= payload.Damage;
            
            if (playerHealth <= 0 && !lost) {
                lost = true;
                EventManager.Trigger(new EventPayloads.BattleEndEvent(false));
                Debug.Log("lost");
            }
        }

        private void OnEnemiesEliminated(EventPayloads.EnemiesEliminated _) {
            if (!lost) {
                EventManager.Trigger(new EventPayloads.BattleEndEvent(true));
                Debug.Log("won");
            }
        }

        public float playerHealth;
        public List<Transform> enemyPath;
        public float PathLength { get; private set; }
        public Grid grid;
        public IngameGold gold;
    }
}