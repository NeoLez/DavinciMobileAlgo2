using UnityEngine;
using UnityEngine.Assertions;

namespace Root.Database
{
    public class Database : MonoBehaviour
    {
        public static Database Ins;

        [Header("Referencias de Sistemas")]
        [SerializeField] public TowerDatabaseSO towerDatabase;
        [SerializeField] public StaminaSystem staminaSystem;
        public CurrencySystem currencySystem;

        private void Awake()
        {
            // Configuro el Singleton para que no se destruya entre escenas
            if (Ins == null)
            {
                Ins = this;

                // Inicializo el sistema de monedas
                currencySystem = new CurrencySystem();
                // Opcional: Le doy 10 monedas de inicio si es la primera vez
                // currencySystem.AddCurrency(10); 

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                // Si ya existe uno, destruyo este para que no haya duplicados
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            // Inicializo mi base de datos de torres (ScriptableObject)
            if (towerDatabase != null)
            {
                towerDatabase.Initialize();
            }

            // Me aseguro de que el sistema de stamina este asignado en el inspector
            Assert.IsTrue(staminaSystem != null, "Yo: Ojo, no asigne el Stamina System en el objeto Database.");
        }

        // Guardo los datos cuando el jugador sale o minimiza el juego
        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                SaveAllData();
            }
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                SaveAllData();
            }
        }

        private void OnApplicationQuit()
        {
            SaveAllData();
        }

        // Metodo centralizado para guardar todo
        private void SaveAllData()
        {
            if (towerDatabase != null) towerDatabase.SaveGame();
            if (currencySystem != null) currencySystem.SaveGame();
            // Aca podria agregar el guardado de stamina si fuera necesario
        }

        // Este es el metodo que llama tu ResetGameData.cs
        public void ResetData()
        {
            Debug.Log("Yo: Iniciando el borrado general de datos...");

            // Reseteo las torres desbloqueadas
            if (towerDatabase != null)
            {
                towerDatabase.ResetData();
            }

            // Reseteo el dinero a cero (o al valor inicial)
            if (currencySystem != null)
            {
                currencySystem.ResetData();
            }

            // Reseteo la energia
            if (staminaSystem != null)
            {
                staminaSystem.ResetData();
            }

            // Obligo a Unity a escribir los cambios en disco ahora mismo
            PlayerPrefs.Save();

            Debug.Log("Yo: Datos borrados con exito.");
        }
    }
}