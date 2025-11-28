using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Root.Gameplay {
    [RequireComponent(typeof(Stats.Stats))]
    public class Enemy : MonoBehaviour {
        [SerializeReference, SubclassSelector] private EnemyMovementBehaviour movementBehaviour;
        private Stats.Stats stats;
        [SerializeField] private int health;

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
            Destroy(gameObject);
        }
    }
}