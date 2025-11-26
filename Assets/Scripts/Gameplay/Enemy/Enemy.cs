using UnityEngine;

namespace Root.Gameplay {
    public class Enemy : MonoBehaviour {
        [SerializeReference, SubclassSelector] private EnemyMovementBehaviour movementBehaviour;
        [SerializeField] private Stats.Stats stats;

        private void Start() {
            stats.Initialize();
            movementBehaviour.Initialize(this);
            
        }

        private void Update() {
            movementBehaviour.Update(Time.deltaTime);
        }

        public Stats.Stats GetStats() {
            return stats;
        }
    }
}