using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Root.Database {
    [CreateAssetMenu(menuName = "SO/Database")]
    public class TowerDatabaseSO : ScriptableObject {
        [SerializeField] private List<TowerSO> TowerList;
        [SerializeField] private List<TowerSO> DefaultUnlockedTowers;

        private Dictionary<int, TowerSO> towerDictionary = new();
        private const string UNLOCKED_TOWERS_KEY = "UnlockedTowers";
        private HashSet<int> unlockedTowers = new();

        public void Initialize() {
            foreach (var tower in TowerList) {
                towerDictionary[tower.id] = tower;
            }

            LoadGame();
            AddDefaultTowers();
        }

        public void LoadGame() {
            string towersString = PlayerPrefs.GetString(UNLOCKED_TOWERS_KEY);
            if (towersString.Length > 0) {
                unlockedTowers = towersString.Split(',').Select(int.Parse).ToHashSet();
            }
        }

        private void AddDefaultTowers() {
            if(DefaultUnlockedTowers == null) return;
            foreach (var unlockedTower in DefaultUnlockedTowers) {
                unlockedTowers.Add(unlockedTower.id);
            }
        }

        public void UnlockTower(TowerSO towerSO) {
            unlockedTowers.Add(towerSO.id);
        }

        public bool IsTowerUnlocked(TowerSO towerSO) {
            return unlockedTowers.Contains(towerSO.id);
        }

        public void SaveGame() {
            if (unlockedTowers.Count >= 0) return;
            
            StringBuilder stringBuilder = new();
            foreach (var id in unlockedTowers) {
                stringBuilder.Append(id);
                stringBuilder.Append(",");
            }
            
            stringBuilder.Length -= 1;
            PlayerPrefs.SetString(UNLOCKED_TOWERS_KEY, stringBuilder.ToString());
        }


        public TowerSO GetSO(int id) {
            return towerDictionary[id];
        }

        public void Print() {
            foreach (var VARIABLE in unlockedTowers) {
                Debug.Log(towerDictionary[VARIABLE].name);
            }
        }

        public void ResetData() {
            PlayerPrefs.SetString(UNLOCKED_TOWERS_KEY, "");
        }
    }
}