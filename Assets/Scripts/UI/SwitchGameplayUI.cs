using UnityEngine;

namespace Root.UI
{
    public class SwitchGameplayUI : MonoBehaviour
    {
        [SerializeField] private GameObject buyMenu;
        [SerializeField] private GameObject upgradeMenu;
        private void Awake()
        {
            EventManager.Subscribe<EventPayloads.GridSelectedTower>(HandleSelectedTower);
        }

        private void HandleSelectedTower(EventPayloads.GridSelectedTower ctx)
        {
            buyMenu.SetActive(false);
            upgradeMenu.GetComponent<UpgradeMenu>().SetInfo(ctx.tower, ctx.gridPosition);
            upgradeMenu.SetActive(true);
        }

        public void SwitchToBuyMenu()
        {
            buyMenu.SetActive(true);
            upgradeMenu.SetActive(false);
        }

        private void OnDestroy()
        {
            EventManager.Unsubscribe<EventPayloads.GridSelectedTower>(HandleSelectedTower);
        }
    }
}