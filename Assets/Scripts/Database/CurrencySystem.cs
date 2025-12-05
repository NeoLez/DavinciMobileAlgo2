using Root.Utils;
using UnityEngine;

namespace Root.Database {
    public class CurrencySystem {
        private const string CURRENT_CURRENCY = "CurrentCurrency";
        private int currency;
        private int startCurrency;
        public int lastReward { get; set; }

        public CurrencySystem() {
            LoadGame();
            startCurrency = RemoteManager.GetInt("startCurrency");
        }
        
        public int GetCurrency() {
            return currency;
        }

        public void AddCurrency(int amount) {
            if (amount <= 0) return;
            
            currency += amount;
        }

        public bool ConsumeCurrency(int amount) {
            Debug.Log($"Current {currency}, to consume {amount}");
            if (amount > currency) return false;

            currency -= amount;
            return true;
        }


        public void LoadGame() {
            currency = PlayerPrefs.GetInt(CURRENT_CURRENCY, startCurrency);
        }

        public void SaveGame() {
            PlayerPrefs.SetInt(CURRENT_CURRENCY, currency);
        }

        public void ResetData()
        {
            currency = startCurrency;
            SaveGame();
        }
    }
}