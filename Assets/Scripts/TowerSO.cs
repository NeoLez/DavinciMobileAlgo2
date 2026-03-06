using System.Collections.Generic;
using Root.Gameplay;
using Root.Utils;
using UnityEngine;

namespace Root {
    [CreateAssetMenu(menuName = "SO/Towers")]
    public class TowerSO : ScriptableObject {
        public int id;
        public Sprite icon;
        public string towerName;
        public string description;
        
        public List<GameObject> levels;
        public List<int> levelCosts = new();
        
        
        public static void InitializeValues() {
            var towerSos = Resources.LoadAll<TowerSO>("Towers");
            foreach (var towerSo in towerSos) {
                towerSo.UpdateValuesRemote();
            }
        }

        public void UpdateValuesRemote() {
            string levelCostsString = RemoteManager.GetString("ID_" + name + "LevelCosts");
            var costs = levelCostsString.Split();
            for (int i = 0; i < costs.Length; i++) {
                levelCosts[i] = int.Parse(costs[i]);
            }
        }
    }
}