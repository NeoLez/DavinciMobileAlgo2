using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Root.Gameplay.Stats {
    [Serializable]
    public class Stats : MonoBehaviour {
        [SerializeField] private List<StatValue> statsEditorOnly;
        private Dictionary<int, StatValue> statsDictionary = new();

        public void Awake() {
            if(statsEditorOnly == null || statsEditorOnly.Count == 0) return;
            
            foreach (var statValue in statsEditorOnly) {
                statsDictionary[statValue.stat.id] = statValue;
            }
        }

        public StatValue GetValue(StatSO statSo) {
            return GetValue(statSo.id);
        }

        public StatValue GetValue(int id) {
            if (statsDictionary.TryGetValue(id, out StatValue statValue)) {
                return statValue;
            }
            Debug.LogWarning("Error. Tried to access unset stat.");
            return null;
        }
        
        public StatValue GetValue(Stat id) {
            return GetValue((int)id);
        }
        
    }
}