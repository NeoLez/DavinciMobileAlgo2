using System;
using Root.Gameplay.Stats;
using UnityEngine;

namespace Root.Gameplay {
    [Serializable]
    public class TowerGenerateMoney : TowerAction
    {
        [SerializeField] private GameObject MoneyImagePrefab;
        public override void Activate() {
            var a = Physics2D.OverlapCircleAll(tower.transform.position, towerStats.GetValue(Stat.TowerRange).value);
            Level.Ins.gold.AddGold((int)towerStats.GetValue(Stat.GoldGenerated).value * a.Length);
            GameObject.Instantiate(MoneyImagePrefab, tower.transform.position, Quaternion.identity);
            Debug.Log("MONEY");
            CompletedAction();
        }
    }
}