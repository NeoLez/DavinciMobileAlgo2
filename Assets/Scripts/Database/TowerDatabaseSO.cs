using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Root.Database
{
    [CreateAssetMenu(menuName = "SO/Database")]
    public class TowerDatabaseSO : ScriptableObject
    {
        [SerializeField] private List<TowerSO> TowerList;
        [SerializeField] private List<TowerSO> DefaultUnlockedTowers;

        private Dictionary<int, TowerSO> towerDictionary = new();
        private const string UNLOCKED_TOWERS_KEY = "UnlockedTowers";
        private HashSet<int> unlockedTowers = new();

        public void Initialize()
        {
            // Limpio el diccionario por si las dudas y lo vuelvo a llenar
            towerDictionary.Clear();
            foreach (var tower in TowerList)
            {
                if (tower != null) towerDictionary[tower.id] = tower;
            }

            LoadGame();
            AddDefaultTowers();
        }

        public void LoadGame()
        {
            // Cargo el string de IDs guardado en el disco
            string towersString = PlayerPrefs.GetString(UNLOCKED_TOWERS_KEY, "");
            if (!string.IsNullOrEmpty(towersString))
            {
                unlockedTowers = towersString.Split(',').Select(int.Parse).ToHashSet();
            }
        }

        public void ResetData()
        {
            // Borro el registro de torres desbloqueadas y limpio la memoria
            PlayerPrefs.DeleteKey(UNLOCKED_TOWERS_KEY);
            unlockedTowers.Clear();

            // Vuelvo a dar las torres que vienen por defecto (las basicas)
            AddDefaultTowers();
            SaveGame();
        }

        private void AddDefaultTowers()
        {
            if (DefaultUnlockedTowers == null) return;
            foreach (var unlockedTower in DefaultUnlockedTowers)
            {
                unlockedTowers.Add(unlockedTower.id);
            }
        }

        public void SaveGame()
        {
            // Armo el string con las IDs separadas por coma para guardar
            StringBuilder stringBuilder = new();
            foreach (var id in unlockedTowers)
            {
                stringBuilder.Append(id);
                stringBuilder.Append(",");
            }

            // Saco la ultima coma si es que hay algo en el builder
            if (stringBuilder.Length > 0) stringBuilder.Length -= 1;

            PlayerPrefs.SetString(UNLOCKED_TOWERS_KEY, stringBuilder.ToString());
            PlayerPrefs.Save();
        }

        public void UnlockTower(TowerSO towerSO)
        {
            if (!unlockedTowers.Contains(towerSO.id))
            {
                unlockedTowers.Add(towerSO.id);
                SaveGame();
            }
        }

        public bool IsTowerUnlocked(TowerSO towerSO) => unlockedTowers.Contains(towerSO.id);

        // Uso esto para que el creador de botones sepa que mostrar
        public List<TowerSO> GetUnlockedTowers()
        {
            return unlockedTowers
                .Where(id => towerDictionary.ContainsKey(id))
                .Select(id => towerDictionary[id])
                .ToList();
        }
    }
}
