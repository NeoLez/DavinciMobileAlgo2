using System;
using Root.FactoryAndPool;
using UnityEngine;

namespace Root.Gameplay {
    [Serializable]
    public class TowerSummonProjectile : TowerAction {
        [SerializeField] private Poolable _projectile;
        public override void Activate() {
            Enemy enemy = tower.GetTargetedEnemy();
            if (enemy != null) {
                Poolable obj = PoolManager.Instance.GetObject(_projectile);
                obj.transform.position = tower.transform.position + Vector3.back;
                Projectile projectile = obj.GetComponent<Projectile>();
                projectile.Initialize(tower, enemy);
                //tower.GetComponentInChildren<TowerSquashFeedback>()?.PlayShootEffect();
                CompletedAction();
            }
        }
    }
}