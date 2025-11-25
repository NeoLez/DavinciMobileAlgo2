using UnityEngine;

namespace Root.Database {
    public class Database : MonoBehaviour {
        [SerializeField] private TowerDatabaseSO towerDatabase;

        private void Start() {
            towerDatabase.Initialize();
            towerDatabase.Print();
        }
        
        private void OnApplicationFocus(bool hasFocus) {
            if (!hasFocus) {
                towerDatabase.SaveGame();
            }
        }
        
        private void OnApplicationPause(bool pause)
        {
            if (pause) {
                towerDatabase.SaveGame();
            }
        }

        private void OnApplicationQuit() {
            towerDatabase.SaveGame();
        }
    }
}