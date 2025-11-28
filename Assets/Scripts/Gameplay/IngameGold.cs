using UnityEngine;

namespace Root.Gameplay {
    public class IngameGold : MonoBehaviour {
        [SerializeField] private int gold;
        
        public int GetGold() {
            return gold;
        }

        public void AddGold(int amount) {
            if (amount <= 0) return;
            
            gold += amount;
        }

        public bool ConsumeGold(int amount) {
            if (amount > gold) return false;

            gold -= amount;
            return true;
        }
    }
}