using System.Collections.Generic;
using Root.Gameplay;
using UnityEngine;

namespace Root {
    [CreateAssetMenu(menuName = "SO/Towers")]
    public class TowerSO : ScriptableObject {
        public int id;
        public Sprite icon;
        public string towerName;
        public string description;
        
        public List<GameObject> levels;
        public List<int> levelCosts;

    }
}