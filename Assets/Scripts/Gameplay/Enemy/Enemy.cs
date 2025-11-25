using System;
using UnityEngine;

namespace Root.Gameplay {
    public class Enemy : MonoBehaviour {
        [SerializeReference, SubclassSelector] private EnemyMovementBehaviour movementBehaviour;

        private void Start() {
            movementBehaviour.Initialize(this);
        }

        private void Update() {
            movementBehaviour.Update(Time.deltaTime);
        }
    }
}