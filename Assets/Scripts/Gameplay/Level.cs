using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        [SerializeField] private string victoryScene;
        [SerializeField] private string defeatScene;
        [SerializeField] private int currencyReward;
        private void TakeDamage(EventPayloads.EnemyReachedEnd payload) {
            playerHealth -= payload.Damage;
            
            if (playerHealth <= 0 && !lost) {
                lost = true;
                EventManager.Trigger(new EventPayloads.BattleEndEvent(false));
                Debug.Log("lost");
                SceneManager.LoadScene(defeatScene);
            }
        }

        private void OnEnemiesEliminated(EventPayloads.EnemiesEliminated _) {
            if (!lost) {
                EventManager.Trigger(new EventPayloads.BattleEndEvent(true));
                Database.Database.Ins.currencySystem.AddCurrency(currencyReward);
                Database.Database.Ins.currencySystem.lastReward = currencyReward;
                SceneManager.LoadScene(victoryScene);
                Debug.Log("won");
            }
        }

        public float playerHealth;
        public List<Transform> enemyPath;
        public float PathLength { get; private set; }
        public Grid grid;
        public IngameGold gold;

        private void OnDestroy()
        {
            EventManager.Unsubscribe<EventPayloads.EnemyReachedEnd>(TakeDamage);
            EventManager.Unsubscribe<EventPayloads.EnemiesEliminated>(OnEnemiesEliminated);
        }
    }
}