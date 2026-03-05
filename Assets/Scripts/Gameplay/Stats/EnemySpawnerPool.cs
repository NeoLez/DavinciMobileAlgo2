using System;
using Root.FactoryAndPool;
using UnityEngine;

namespace Root.Gameplay.Stats {
    [Serializable]
    public class EnemySpawnerPool {
        [SerializeField] public Poolable enemy;
        [SerializeField] public int amount;
        [SerializeField] public float time;
    }
}