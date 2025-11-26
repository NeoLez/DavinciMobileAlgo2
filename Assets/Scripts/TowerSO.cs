using UnityEngine;

namespace Root {
    [CreateAssetMenu(menuName = "SO/Towers")]
    public class TowerSO : ScriptableObject {
        public int id;
        public string towerName;
    }
}