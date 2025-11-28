using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Root.Gameplay {
    [Serializable]
    public class TowerSummonProjectile : TowerAction {
        [SerializeField] private GameObject _projectile;
        public override void Activate() {
            Enemy enemy = tower.GetTargetedEnemy();
            if (enemy != null) {
                Debug.Log("b");
                GameObject obj = Object.Instantiate(_projectile);
                obj.transform.position = tower.transform.position + Vector3.back;
                IProjectile projectile = obj.GetComponent<IProjectile>();
                projectile.Initialize(tower, enemy);
                CompletedAction();
            }
        }
    }
}