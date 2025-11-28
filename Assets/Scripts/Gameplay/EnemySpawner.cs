using System;
using System.Collections.Generic;
using Root.Gameplay.Stats;
using UnityEngine;

namespace Root.Gameplay {
    public class EnemySpawner : MonoBehaviour {
        [SerializeField] private List<EnemySpawnerPool> waves;
        [SerializeField]private int waveIndex;
        [SerializeField]private bool wavesFinished;

        [SerializeField]private float waveFinishTime;
        [SerializeField]private float nextEnemySpawn = float.MaxValue;
        [SerializeField]private float enemySpawnDelay;

        [SerializeField]private int totalSpawnedEnemies;

        private void Awake() {
            EventManager.Subscribe<EventPayloads.EnemyDied>((_ => totalSpawnedEnemies--));
        }

        private void Update() {
            if (wavesFinished) {
                if (totalSpawnedEnemies == 0) {
                    EventManager.Trigger(new EventPayloads.BattleEndEvent(true));
                }
                return;
            }
            if (Time.time > waveFinishTime) {
                if (waveIndex == waves.Count - 1) {
                    wavesFinished = true;
                    return;
                }
                
                waveIndex++;
                waveFinishTime = Time.time + waves[waveIndex].time;
                int amount = waves[waveIndex].amount;
                if (amount == 0) {
                    enemySpawnDelay = float.MaxValue;
                }
                else {
                    enemySpawnDelay = waves[waveIndex].time / waves[waveIndex].amount;
                }
                
                nextEnemySpawn = Time.time + enemySpawnDelay;
            }

            if (Time.time >= nextEnemySpawn) {
                nextEnemySpawn = Time.time + nextEnemySpawn;
                GameObject enemy = Instantiate(waves[waveIndex].enemy);
                enemy.transform.position = transform.position;
                totalSpawnedEnemies++;
            }
            
        }

        public bool HaveWavesFinished() {
            return wavesFinished;
        }
    }
}