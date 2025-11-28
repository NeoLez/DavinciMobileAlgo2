using System;
using System.Collections.Generic;
using Root.Gameplay.Stats;
using UnityEngine;

namespace Root.Gameplay {
    [Serializable]
    public class NormalEnemyMovementBehaviour : EnemyMovementBehaviour {
        private List<Transform> waypoints;
        private StatValue movementSpeed;
        public override void Initialize(Enemy enemy) {
            base.Initialize(enemy);
            waypoints = Level.Ins.enemyPath;
            movementSpeed = enemy.GetStats().GetValue(Stat.MovementSpeed);
        }

        private int index;
        public override void Update(float deltaTime) {
            float moveAmount = movementSpeed.value * deltaTime;
            
            
            while (index < waypoints.Count && moveAmount > 0) {
                Vector3 moveDiff = waypoints[index].position - enemy.transform.position;
                float distanceToWaypoint = moveDiff.magnitude;
                Vector3 moveDir = moveDiff.normalized;

                enemy.transform.position += moveDir * Mathf.Min(moveAmount, distanceToWaypoint);
                totalDistanceTravelled += Mathf.Min(moveAmount, distanceToWaypoint);
                
                moveAmount -= distanceToWaypoint;
                if (moveAmount >= 0) {
                    index++;
                }
            }

            if (index >= waypoints.Count) {
                EventManager.Trigger(new EventPayloads.EnemyReachedEnd((int)enemy.GetStats().GetValue(Stat.AttackDamage).value));
                EventManager.Trigger(new EventPayloads.EnemyDied());
                GameObject.Destroy(enemy.gameObject);
            }
        }

        public override float GetPathPercentageCompletion() {
            return totalDistanceTravelled / Level.Ins.PathLength;
        }
    }
}