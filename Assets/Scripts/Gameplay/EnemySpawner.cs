using System;
using System.Collections.Generic;
using Root.Gameplay.Stats;
using UnityEngine;

namespace Root.Gameplay {
    public class EnemySpawner : MonoBehaviour {
        [SerializeField] private List<EnemySpawnerPool> waves;
        [SerializeField] private int waveIndex;
        [SerializeField] private bool wavesFinished;
        [SerializeField] private int enemiesSpawnedThisWave;
        [SerializeField] private bool running; 
        
        [SerializeField] private int totalSpawnedEnemies;
        private float lastTimeSpawned;
        private bool raisedWin;

        private void Awake() {
            EventManager.Subscribe<EventPayloads.EnemyDied>(OnEnemyDied);
        }

        private void OnEnemyDied(EventPayloads.EnemyDied _)
        {
            totalSpawnedEnemies--;
        }

        private void Update() {
            if(!running) return;
            if (wavesFinished) {
                if (totalSpawnedEnemies == 0 && !raisedWin) {
                    raisedWin = true;
                    EventManager.Trigger(new EventPayloads.EnemiesEliminated());
                }
                return;
            }
            
            if (Time.time >= lastTimeSpawned + waves[waveIndex].time && enemiesSpawnedThisWave < waves[waveIndex].amount) {
                GameObject enemy = Instantiate(waves[waveIndex].enemy);
                enemy.transform.position = transform.position;
                enemiesSpawnedThisWave++;
                totalSpawnedEnemies++;
                lastTimeSpawned = Time.time;
                if (enemiesSpawnedThisWave == waves[waveIndex].amount) {
                    enemiesSpawnedThisWave = 0;
                    waveIndex++;
                    if (waveIndex == waves.Count) {
                        wavesFinished = true;
                    }
                }
            } 
        }

        public bool HaveWavesFinished() {
            return wavesFinished;
        }

        private void OnDestroy()
        {
            EventManager.Unsubscribe<EventPayloads.EnemyDied>(OnEnemyDied);
        }
    }
}