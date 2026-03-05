using Root.FactoryAndPool;

namespace Root.Gameplay {
    public abstract class Projectile : Poolable {
        public abstract void Initialize(Tower tower, Enemy enemy);
    }
}