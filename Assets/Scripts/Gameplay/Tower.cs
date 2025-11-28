using Root.Gameplay.Stats;
using UnityEngine;

namespace Root.Gameplay {
    [RequireComponent(typeof(Stats.Stats), typeof(CircleCollider2D))]
    public class Tower : MonoBehaviour {
        private Stats.Stats stats;
        [SerializeReference, SubclassSelector] private TowerAction action;
        private Enemy targetedEnemy;
        private float targetDistance = float.MaxValue;
        [SerializeField] private TowerSO TowerSO;
        [SerializeField] private int upgradeLevel;
        private void Start() {
            stats = GetComponent<Stats.Stats>();
            GetComponent<CircleCollider2D>().radius = stats.GetValue(Stat.TowerRange).value;
            action.Initialize(this);
        }

        private void Update() {
            action.Update();
        }

        public Stats.Stats GetStats() {
            return stats;
        }

        public TowerSO GetTowerSO() {
            return TowerSO;
        }

        public int GetUpgradeLevel() {
            return upgradeLevel;
        }

        public Enemy GetTargetedEnemy() {
            return targetedEnemy;
        }

        [SerializeField] private LayerMask enemyLayer;
        private void OnTriggerStay2D(Collider2D other) {
            if (other.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;
            
            float dist = Vector2.Distance(transform.position, other.transform.position);
            if (dist < targetDistance) {
                targetedEnemy = other.GetComponent<Enemy>();
                targetDistance = dist;
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (targetedEnemy == null || other.gameObject == null) return;
            
            Debug.Log(targetedEnemy);
            Debug.Log(targetedEnemy.gameObject);
            Debug.Log(other);
            Debug.Log(other.gameObject);
            
            if (targetedEnemy.gameObject == other.gameObject) {
                targetedEnemy = null;
                targetDistance = float.MaxValue;
                //TODO could cause a bug since it doesn't recalculate closest enemy when it removes the previous one if the action gets invoked in the same frame
            }
        }
    }
}