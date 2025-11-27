using UnityEngine;

namespace Root.Database {
    public class CurrencySystem {
        private const string CURRENT_CURRENCY = "CurrentCurrency";
        private int currency;
        private const int START_CURRENCY = 0;

        public CurrencySystem() {
            LoadGame();
        }


        public int GetCurrency() {
            return currency;
        }

        public void AddCurrency(int amount) {
            if (amount <= 0) return;
            
            currency += amount;
        }

        public bool ConsumeCurrency(int amount) {
            if (amount > currency) return false;

            currency -= amount;
            return true;
        }


        public void LoadGame() {
            currency = PlayerPrefs.GetInt(CURRENT_CURRENCY, START_CURRENCY);
        }

        public void SaveGame() {
            PlayerPrefs.SetInt(CURRENT_CURRENCY, currency);
        }
    }
}