using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Root.UI
{
    public class TowerSelectionButton : MonoBehaviour
    {
        [SerializeField] private TowerSO towerSo;
        [SerializeField] private TextTranslate towerName;
        [SerializeField] private TextTranslate description;
        [SerializeField] private Image image;

        public void SetTower(TowerSO towerSo)
        {
            this.towerSo = towerSo;
            image.sprite = towerSo.icon;
            towerName.SetId(towerSo.towerName);
            description.SetId(towerSo.description);
        }

        public void OnClick()
        {
            EventManager.Trigger(new EventPayloads.MenuSelectedTowerSO(towerSo));
        }
    }
}