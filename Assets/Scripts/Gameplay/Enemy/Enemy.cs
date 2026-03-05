using Root.FactoryAndPool;
using Root.Gameplay.Stats;
using UnityEngine;
using UnityEngine.Assertions;

namespace Root.Gameplay {
    [RequireComponent(typeof(Stats.Stats))]
    public class Enemy : Poolable {
        [SerializeReference, SubclassSelector] private EnemyMovementBehaviour movementBehaviour;
        private Stats.Stats stats;
        [SerializeField] private int health;
        [SerializeField] private int cashReward;
        [SerializeField] private Poolable MoneyImagePrefab;
        private bool isDead;

        private void Start() {
            stats = GetComponent<Stats.Stats>();
            movementBehaviour.Initialize(this);
            
        }

        private void Update() {
            movementBehaviour.Update(Time.deltaTime);
        }

        public float GetPathPercentageCompletion() {
            return movementBehaviour.GetPathPercentageCompletion();
        }

        public Stats.Stats GetStats() {
            return stats;
        }

        public void Initialize(Vector3 position) {
            transform.position = position;
            health = (int)stats.GetValue(Stat.MaxHealth).value;
        }

        /// <summary>
        /// Returns leftover damage
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public int TakeDamage(int amount) {
            Assert.IsTrue(amount > 0);
            health -= amount;
            if (health <= 0) {
                Die();
                return Mathf.Abs(health);
            }

            return 0;
        }

        public void Die() {
            
            if(isDead) return;
            isDead = true;

            if (cashReward > 0)
            {
                var coins = PoolManager.Instance.GetObject(MoneyImagePrefab);
                coins.transform.position = transform.position;
                Level.Ins.gold.AddGold(cashReward);
            }
            
            
            EventManager.Trigger(new EventPayloads.EnemyDied());
            TurnOff();
        }
    }
}