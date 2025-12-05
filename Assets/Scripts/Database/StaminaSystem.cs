using System;
using Root.Utils;
using UnityEngine;
using UnityEngine.Assertions;

namespace Root {
    public class StaminaSystem : MonoBehaviour {
        private long lastStaminaTime;

        [SerializeField] private int maxStamina = 100;
        public int currentStamina;

        [SerializeField] private int regenerationTime = 60;
        private bool isRegenerating;
        [SerializeField] private bool shouldStartFull;
        
        private const string CURRENT_STAMINA =  "CurrentStamina";
        private const string LAST_STAMINA_TIME =  "LastStaminaTime";

        private void Start() {
            LoadGame();
            DontDestroyOnLoad(this);

            shouldStartFull = RemoteManager.GetBool("staminaShouldStartFull");
            regenerationTime = RemoteManager.GetInt("staminaRegenerationTime");
            maxStamina = RemoteManager.GetInt("maxStamina");
        }

        private void Update() {
            if (isRegenerating) {
                UpdateStamina();
            }
        }

        private void UpdateStamina() {
            long timeDifference = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - lastStaminaTime;
            if (timeDifference < regenerationTime) return;

            int amountRegenerated = (int)(timeDifference / regenerationTime);
            if (amountRegenerated > 0) {
                AddStamina(amountRegenerated);
                lastStaminaTime += amountRegenerated * regenerationTime;
                if (IsStaminaFull()) {
                    StopRegeneration();
                }
            }
        }

        public void AddStamina(int amount) {
            Assert.IsTrue(amount > 0);
            
            currentStamina = Mathf.Clamp(currentStamina + amount, 0, maxStamina);
        }

        public bool ConsumeStamina(int amount) {
            Assert.IsTrue(amount > 0);
            if(currentStamina < amount) return false;

            if (IsStaminaFull()) {
                StartRegeneration();
            }
            
            currentStamina -= amount;
            return true;
        }

        private void StartRegeneration() {
            lastStaminaTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + regenerationTime;
            isRegenerating = true;
        }

        private void StopRegeneration() {
            isRegenerating = false;
        }

        public bool IsStaminaFull() {
            return currentStamina == maxStamina;
        }
        
        private void LoadGame() {
            if (shouldStartFull)
            {
                currentStamina = PlayerPrefs.GetInt(CURRENT_STAMINA, maxStamina);
            }
            else
            {
                currentStamina = PlayerPrefs.GetInt(CURRENT_STAMINA, 0);
            }
            
            lastStaminaTime = long.Parse(PlayerPrefs.GetString(LAST_STAMINA_TIME, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()));
            if (IsStaminaFull()) {
                StopRegeneration();
            }
            else {
                isRegenerating = true;
                UpdateStamina();
            }
        }

        private void SaveGame() {
            PlayerPrefs.SetInt(CURRENT_STAMINA, currentStamina);
            PlayerPrefs.SetString(LAST_STAMINA_TIME, lastStaminaTime.ToString());
        }

        public int GetStamina() {
            return currentStamina;
        }
        
        public int GetMaxStamina() {
            return maxStamina;
        }

        public void ResetData()
        {
            currentStamina = maxStamina;
            SaveGame();
        }

        public int GetSecondsUntilRegen() {
            return (int)(lastStaminaTime + regenerationTime - DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        }

        private void OnApplicationFocus(bool hasFocus) {
            if (!hasFocus) {
                SaveGame();
            }
        }
        
        private void OnApplicationPause(bool pause)
        {
            if (pause) {
                SaveGame();
            }
        }

        private void OnApplicationQuit() {
            SaveGame();
        }
    }
}