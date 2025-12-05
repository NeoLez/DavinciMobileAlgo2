using TMPro;
using UnityEngine;

namespace Root
{
    public class SetTextToLastCurrencyReward : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<TMP_Text>().text = Database.Database.Ins.currencySystem.lastReward.ToString();
        }
    }
}
