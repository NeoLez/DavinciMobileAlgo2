using UnityEngine;

namespace Root.Gameplay {
    [RequireComponent(typeof(Stats.Stats))]
    public class Enemy : MonoBehaviour {
        [SerializeReference, SubclassSelector] private EnemyMovementBehaviour movementBehaviour;
        private Stats.Stats stats;

        private void Start() {
            stats = GetComponent<Stats.Stats>();
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