using System;
using System.Collections.Generic;
using UnityEngine;

namespace Root.Gameplay {
    [Serializable]
    public class NormalEnemyMovementBehaviour : EnemyMovementBehaviour {
        [SerializeField] private float speed;
        private List<Transform> waypoints;
        public override void Initialize(Enemy enemy) {
            base.Initialize(enemy);
            waypoints = Level.Ins.enemyPath;
            Debug.Log(waypoints.Count);
        }

        private int index;
        public override void Update(float deltaTime) {
            float moveAmount = speed * deltaTime;

            while (index < waypoints.Count && moveAmount > 0) {
                Vector3 moveDiff = waypoints[index].position - enemy.transform.position;
                float distanceToWaypoint = moveDiff.magnitude;
                Vector3 moveDir = moveDiff.normalized;

                enemy.transform.position += moveDir * Mathf.Min(moveAmount, distanceToWaypoint);
                moveAmount -= distanceToWaypoint;
                if (moveAmount >= 0) {
                    index++;
                }
            }
        }
    }
}