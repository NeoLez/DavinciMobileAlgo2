using System;
using System.Collections.Generic;
using UnityEngine;

namespace Root.Gameplay.Stats {
    [Serializable]
    public class Stats {
        [SerializeField] private List<StatValue> statsEditorOnly;
        private Dictionary<int, StatValue> statsDictionary = new();

        public void Initialize() {
            foreach (var statValue in statsEditorOnly) {
                Debug.Log($"{statValue.stat.id} {statValue.stat.statName}");
                statsDictionary[statValue.stat.id] = statValue;
            }
        }

        public StatValue GetValue(StatSO statSo) {
            return GetValue(statSo.id);
        }

        public StatValue GetValue(int id) {
            return statsDictionary[id];
        }
    }
}