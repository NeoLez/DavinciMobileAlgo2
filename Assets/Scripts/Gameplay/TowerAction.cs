using System;
using Root.Gameplay.Stats;
using UnityEngine;

namespace Root.Gameplay {
    [Serializable]
    public abstract class TowerAction {
        protected Tower tower;
        protected Stats.Stats towerStats;
        protected float lastActivation;
        private StatValue cooldown;    
        
        public void Initialize(Tower tower) {
            this.tower = tower;
            towerStats = tower.GetStats();
            lastActivation = Time.time;
            cooldown = towerStats.GetValue(Stat.AttackCooldown);
        }

        public void Update() {
            //Debug.Log($"{Time.time} {lastActivation} {cooldown.value}");
            if (Time.time >= lastActivation + cooldown.value) {
                Activate();
            }
        }

        public abstract void Activate();

        protected void CompletedAction() {
            lastActivation = Time.time;
        }
    }
}