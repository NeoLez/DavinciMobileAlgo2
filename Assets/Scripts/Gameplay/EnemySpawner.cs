using UnityEngine;

namespace Root.Gameplay {
    public class EnemySpawner : MonoBehaviour {
        [SerializeField] private bool wavesFinished;
        [SerializeField] private bool running; 
        [SerializeField] private int totalSpawnedEnemies;
        [SerializeField] private EnemyFactory _factory;
        [SerializeField] private float delayBetweenWaves;
        
        private bool raisedWin;

        private void Awake() {
            EventManager.Subscribe<EventPayloads.EnemyDied>(OnEnemyDied);
            _factory.OnWaveFinished += () => {
                if (_factory.HasFinished()) {
                    wavesFinished = true;
                }
            };
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

            if (!_factory.IsReadyToSpawn()) return;
            
            var enemy = _factory.GetEnemy();
            if (enemy == null) return;
            enemy.transform.position = transform.position;
            totalSpawnedEnemies++;
        }

        public bool HaveWavesFinished() {
            return wavesFinished;
        }

        private void OnDestroy()
        {
            EventManager.Unsubscribe<EventPayloads.EnemyDied>(OnEnemyDied);
        }

        
        public void StartSpawnerManual()
        {
            running = true;
        }

    }
}