using System.Collections.Generic;
using UnityEngine;

namespace Root.UI
{
    public class TowerSelectionButtonCreator  : MonoBehaviour
    {
        [SerializeField] private TowerSelectionButton buttonPrefab;
        private void Start()
        {
            List<TowerSO> unlockedTowers = Database.Database.Ins.towerDatabase.GetUnlockedTowers();
            foreach (var unlockedTower in unlockedTowers)
            {
                TowerSelectionButton button = Instantiate(buttonPrefab, transform);
                button.SetTower(unlockedTower);
            }
        }
    }
}