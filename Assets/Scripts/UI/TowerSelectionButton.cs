using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Root.UI
{
    public class TowerSelectionButton : MonoBehaviour
    {
        [SerializeField] private TowerSO towerSo;
        [SerializeField] private TMP_Text towerName;
        [SerializeField] private TMP_Text description;
        [SerializeField] private Image image;

        public void SetTower(TowerSO towerSo)
        {
            this.towerSo = towerSo;
            towerName.text = towerSo.name;
            description.text = towerSo.description;
            image.sprite = towerSo.icon;
        }

        public void OnClick()
        {
            EventManager.Trigger(new EventPayloads.MenuSelectedTowerSO(towerSo));
        }
    }
}