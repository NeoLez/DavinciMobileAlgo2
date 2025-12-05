using TMPro;
using UnityEngine;

namespace Root
{
    [RequireComponent(typeof(TMP_Text))]
    public class UpdateMoney : MonoBehaviour
    {
        private TMP_Text text;
        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }

        void Update()
        {
            text.text = "$ " + Database.Database.Ins.currencySystem.GetCurrency();
        }
    }
}
