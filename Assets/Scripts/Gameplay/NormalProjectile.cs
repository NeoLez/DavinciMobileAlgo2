using Root.Gameplay.Stats;
using UnityEngine;

namespace Root.Gameplay {
    public class NormalProjectile : Projectile {
        private Vector2 _direction;
        private float _lifetime = 1;
        [SerializeField] private float speed;
        [SerializeField] private GameObject visuals;
        private Tower _tower;
        private float _creationTime;
        private int _pierce;
        private int _damage;
        
        public override void Initialize(Tower tower, Enemy enemy) {
            _direction = (enemy.transform.position - tower.transform.position).normalized;
            _creationTime = Time.time;
            visuals.transform.rotation = Quaternion.LookRotation(_direction);
            _tower = tower;
            Stats.Stats stats = tower.GetStats();
            _pierce = (int)stats.GetValue(Stat.AttackPierceLevel).value;
            _damage = (int)stats.GetValue(Stat.AttackDamage).value;
            _lifetime = stats.GetValue(Stat.TowerRange).value / speed;
        }

        private void Update() {
            if (Time.time > _creationTime + _lifetime) {
                TurnOff();
                return;
            }
            transform.position += (Vector3) _direction * (speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(_damage);
            _pierce--;
            
            if(_pierce == 0) TurnOff();
        }
    }
}