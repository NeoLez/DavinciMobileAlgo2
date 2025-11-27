using System;
using Root.Gameplay.Stats;
using UnityEngine;

namespace Root.Gameplay {
    [RequireComponent(typeof(Stats.Stats), typeof(CircleCollider2D))]
    public class Tower : MonoBehaviour {
        private Stats.Stats stats;
        [SerializeReference, SubclassSelector] private TowerAction action;
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
        
        private void OnTriggerStay2D(Collider2D other) {
            Debug.Log("Triggereado");
        }
    }
}