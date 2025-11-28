using System;
using UnityEngine;

namespace Root.Gameplay.Stats {
    [Serializable]
    public class EnemySpawnerPool {
        [SerializeField] public GameObject enemy;
        [SerializeField] public int amount;
        [SerializeField] public float time;
    }
}