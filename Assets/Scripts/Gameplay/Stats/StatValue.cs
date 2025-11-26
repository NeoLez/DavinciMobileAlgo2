using System;
using UnityEngine;

namespace Root.Gameplay.Stats {
    [Serializable]
    public class StatValue {
        [SerializeField] public StatSO stat;
        [SerializeField] public float value;
    }
}