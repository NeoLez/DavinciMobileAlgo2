using UnityEngine;
using UnityEngine.Assertions;

namespace Root.Database
{
    public class Database : MonoBehaviour
    {
        public static Database Ins;

        [Header("Sistemas")]
        [SerializeField] public TowerDatabaseSO towerDatabase;
        [SerializeField] public StaminaSystem staminaSystem;
        public CurrencySystem currencySystem;

        private void Awake()
        {
            // Configuro el Singleton para que dure toda la partida
            if (Ins == null)
            {
                Ins = this;
                currencySystem = new CurrencySystem();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            if (towerDatabase != null) towerDatabase.Initialize();
            Assert.IsTrue(staminaSystem != null, "Yo: Me olvide de asignar el sistema de Stamina en el Inspector.");
        }

        public void ResetData()
        {
            Debug.Log("Yo: Reseteando toda la base de datos...");

            // Limpio cada sistema individualmente
            towerDatabase.ResetData();
            currencySystem.ResetData();
            if (staminaSystem != null) staminaSystem.ResetData();

            PlayerPrefs.Save();

            // ¡IMPORTANTE! Mando un evento de "Enemigos Eliminados" o similar 
            // para que los scripts de la tienda se refresquen solos sin cambiar de escena
            EventManager.Trigger(new EventPayloads.EnemiesEliminated());

            Debug.Log("Yo: Datos borrados. La UI deberia actualizarse sola.");
        }

        // Guardo todo cada vez que el jugador sale o pausa
        private void OnApplicationQuit() => SaveAll();
        private void OnApplicationPause(bool pause) { if (pause) SaveAll(); }

        private void SaveAll()
        {
            if (towerDatabase != null) towerDatabase.SaveGame();
            if (currencySystem != null) currencySystem.SaveGame();
        }
    }
}
