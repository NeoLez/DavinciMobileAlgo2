using Root.Gameplay.Stats;
using UnityEngine;

namespace Root.Gameplay {
    public class NormalProjectile : MonoBehaviour, IProjectile {
        private Vector2 direction;
        private float lifetime = 1;
        [SerializeField] private float speed;
        [SerializeField] private GameObject visuals;
        private Tower tower;
        private float creationTime;
        private int pierce;
        private int damage;
        public void Initialize(Tower tower, Enemy enemy) {
            direction = (enemy.transform.position - tower.transform.position).normalized;
            creationTime = Time.time;
            visuals.transform.rotation = Quaternion.LookRotation(direction);
            this.tower = tower;
            Stats.Stats stats = tower.GetStats();
            pierce = (int)stats.GetValue(Stat.AttackPierceLevel).value;
            damage = (int)stats.GetValue(Stat.AttackDamage).value;
            lifetime = stats.GetValue(Stat.TowerRange).value / speed;
        }

        private void Update() {
            if (Time.time > creationTime + lifetime) {
                Destroy(gameObject);
                return;
            }
            transform.position += (Vector3) direction * (speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            pierce--;
            
            if(pierce == 0) Destroy(gameObject);
        }
    }
}