using System;
using UnityEngine;

namespace Root.UI
{
    public class SwitchGameplayUI : MonoBehaviour
    {
        [SerializeField] private GameObject buyMenu;
        [SerializeField] private GameObject upgradeMenu;
        private void Awake()
        {
            EventManager.Subscribe<EventPayloads.GridSelectedTower>(a =>
            {
                buyMenu.SetActive(false);
                upgradeMenu.GetComponent<UpgradeMenu>().SetInfo(a.tower, a.gridPosition);
                upgradeMenu.SetActive(true);
            });
        }

        public void SwitchToBuyMenu()
        {
            buyMenu.SetActive(true);
            upgradeMenu.SetActive(false);
        }
    }
}