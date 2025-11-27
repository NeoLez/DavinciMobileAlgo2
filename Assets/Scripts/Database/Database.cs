using UnityEngine;
using UnityEngine.Assertions;

namespace Root.Database {
    public class Database : MonoBehaviour {
        public static Database Ins;
        private void Awake() {
            Ins = this;
            DontDestroyOnLoad(this);
        }

        [SerializeField] public TowerDatabaseSO towerDatabase;
        [SerializeField] public StaminaSystem staminaSystem;
        public CurrencySystem currencySystem;

        private void Start() {
            towerDatabase.Initialize();
            currencySystem = new ();
            Assert.IsTrue(staminaSystem != null, "Database: Stamina system not found.");
        }
        
        private void OnApplicationFocus(bool hasFocus) {
            if (!hasFocus) {
                towerDatabase.SaveGame();
                currencySystem.SaveGame();
            }
        }
        
        private void OnApplicationPause(bool pause)
        {
            if (pause) {
                towerDatabase.SaveGame();
                currencySystem.SaveGame();
            }
        }

        private void OnApplicationQuit() {
            towerDatabase.SaveGame();
            currencySystem.SaveGame();
        }
    }
}