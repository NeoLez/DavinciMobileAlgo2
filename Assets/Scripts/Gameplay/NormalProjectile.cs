using UnityEngine;

namespace Root.Gameplay {
    public class NormalProjectile : MonoBehaviour, IProjectile {
        private Vector2 direction;
        [SerializeField] private float lifetime;
        [SerializeField] private float speed;
        [SerializeField] private GameObject visuals;
        private float creationTime;
        public void Initialize(Tower tower, Enemy enemy) {
            direction = (enemy.transform.position - tower.transform.position).normalized;
            creationTime = Time.time;
            visuals.transform.rotation = Quaternion.LookRotation(direction);
        }

        private void Update() {
            if (Time.time > creationTime + lifetime) {
                Destroy(this);
                return;
            }
            transform.position += (Vector3) direction * (speed * Time.deltaTime);
        }
    }
}