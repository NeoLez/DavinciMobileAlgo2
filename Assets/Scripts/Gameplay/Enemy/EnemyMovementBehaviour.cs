using System;

namespace Root.Gameplay {
    [Serializable]
    public abstract class EnemyMovementBehaviour {
        protected Enemy enemy;

        public virtual void Initialize(Enemy enemy) {
            this.enemy = enemy;
        }
        public abstract void Update(float deltaTime);
    }
}