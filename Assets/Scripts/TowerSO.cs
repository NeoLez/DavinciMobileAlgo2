using UnityEngine;

namespace Root {
    [CreateAssetMenu(menuName = "SO/Towers")]
    public class TowerSO : ScriptableObject {
        public int id;
        public Sprite icon;
        public string towerName;
        public string description;
        public int inGameCost;
        
    }
}