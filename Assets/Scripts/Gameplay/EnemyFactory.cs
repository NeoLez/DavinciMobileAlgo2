using System;
using System.Collections.Generic;
using Root.FactoryAndPool;
using Root.Gameplay.Stats;
using UnityEngine;

namespace Root.Gameplay {
    public class EnemyFactory : MonoBehaviour {
        [SerializeField] private List<EnemySpawnerPool> waves;
        [SerializeField] private int waveIndex;
        [SerializeField] private int enemiesSpawnedThisWave;
        [SerializeField] private bool wavesFinished;
        private float lastTimeSpawned;

        public event Action OnWaveFinished;

        public Enemy GetEnemy() {
            if (IsReadyToSpawn()) {
                var enemy = (Enemy) PoolManager.Instance.GetObject(waves[waveIndex].enemy);
                enemy.InitializeEnemy();
                enemiesSpawnedThisWave++;
                lastTimeSpawned = Time.time;
                if (enemiesSpawnedThisWave == waves[waveIndex].amount) {
                    enemiesSpawnedThisWave = 0;
                    waveIndex++;
                    if (waveIndex == waves.Count) {
                        wavesFinished = true;
                    }
                    OnWaveFinished?.Invoke();
                }

                return enemy;
            }

            return null;
        }

        public bool IsReadyToSpawn() {
            return Time.time >= lastTimeSpawned + waves[waveIndex].time &&
                   enemiesSpawnedThisWave < waves[waveIndex].amount;
        }

        public bool HasFinished() {
            return wavesFinished;
        }
    }
}