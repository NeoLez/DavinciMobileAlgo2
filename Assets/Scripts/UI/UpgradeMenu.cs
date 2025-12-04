using Root.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Grid = Root.Gameplay.Grid;

namespace Root.UI
{
    public class UpgradeMenu : MonoBehaviour
    {
        [SerializeField] private TMP_Text towerName;
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text towerdescription;
        [SerializeField] private GameObject statsPanel;
        [SerializeField] private TMP_Text sellButton;
        [SerializeField] private TMP_Text upgradeButton;

        [SerializeField] private SwitchGameplayUI gameplayUI;

        private Tower tower;
        private Vector2 gridPos;
        
        public void SetInfo(Tower tower, Vector2 gridPos)
        {
            this.tower = tower;
            this.gridPos = gridPos;
            TowerSO so = tower.GetTowerSO();
            towerName.text = so.towerName;
            icon.sprite = so.icon;
            towerdescription.text = so.description;
            int level = tower.GetUpgradeLevel();
            sellButton.text = (so.levelCosts[level] / 2).ToString();
            if (level + 1 < so.levels.Count)
                upgradeButton.text = so.levelCosts[tower.GetUpgradeLevel()].ToString();
            else
                upgradeButton.text = "Max";
        }

        public void Sell()
        {
            Level.Ins.gold.AddGold(tower.GetTowerSO().levelCosts[tower.GetUpgradeLevel()] / 2);
            Grid.Ins.RemoveTower(gridPos);
            gameplayUI.SwitchToBuyMenu();
        }

        public void Upgrade()
        {
            if (Grid.Ins.Upgrade(gridPos))
            {
                SetInfo(Grid.Ins.GetTower(gridPos), gridPos);
            }
        }
    }
}