using System;
using Root.Gameplay.Stats;
using UnityEngine;

namespace Root.Gameplay {
    [RequireComponent(typeof(Stats.Stats), typeof(CircleCollider2D))]
    public class Tower : MonoBehaviour {
        private Stats.Stats stats;
        private void Start() {
            stats = GetComponent<Stats.Stats>();
            GetComponent<CircleCollider2D>().radius = stats.GetValue(Stat.TowerRange).value;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            Debug.Log("Triggereado");
        }
    }
}