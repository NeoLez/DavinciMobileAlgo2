using System;
using UnityEngine;

namespace Root.Gameplay {
    [Serializable]
    public class AcceleratingEnemyMovemengBehaviour : NormalEnemyMovementBehaviour {
        [SerializeField] public float speedIncrease;
        public override void Update(float deltaTime) {
            movementSpeed += speedIncrease * deltaTime;
            base.Update(deltaTime);
        }
    }
}