using System;
using Root.FactoryAndPool;
using Root.Gameplay.Stats;
using UnityEngine;

namespace Root.Gameplay {
    [Serializable]
    public class TowerGenerateMoney : TowerAction
    {
        [SerializeField] private Poolable MoneyImagePrefab;
        public override void Activate() {
            var a = Physics2D.OverlapCircleAll(tower.transform.position, towerStats.GetValue(Stat.TowerRange).value);
            Level.Ins.gold.AddGold((int)towerStats.GetValue(Stat.GoldGenerated).value * a.Length);
            var obj = PoolManager.Instance.GetObject(MoneyImagePrefab);
            obj.transform.position = tower.transform.position;
            tower.GetComponentInChildren<TowerSquashFeedback>()?.PlayShootEffect();
            CompletedAction();
        }
    }
}