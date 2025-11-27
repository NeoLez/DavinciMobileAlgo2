using UnityEngine;

namespace Root.Database {
    public class CurrencyTest : MonoBehaviour {
        public bool get;
        public bool add;
        public bool consume;
        public int amount;

        private void Update() {
            if (get) {
                get = false;
                Debug.Log(Database.Ins);
                Debug.Log(Database.Ins.currencySystem);
                Debug.Log(Database.Ins.currencySystem.GetCurrency());
            }
            if (add) {
                add = false;
                Database.Ins.currencySystem.AddCurrency(amount);
            }
            if (consume) {
                consume = false;
                Debug.Log(Database.Ins.currencySystem.ConsumeCurrency(amount));
            }
        }
    }
}