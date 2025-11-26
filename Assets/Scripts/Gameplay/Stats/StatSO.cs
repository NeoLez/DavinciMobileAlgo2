using UnityEngine;

namespace Root.Gameplay.Stats {
    [CreateAssetMenu(menuName = "SO/Stat")]
    public class StatSO : ScriptableObject {
        public string statName;
        public int id;
    }
}