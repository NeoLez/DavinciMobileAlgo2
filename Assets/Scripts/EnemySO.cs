using System;
using Root.FactoryAndPool;
using Root.Utils;
using UnityEngine;

namespace Root {
    [CreateAssetMenu(menuName = "SO/Enemy")]
    public class EnemySO : ScriptableObject {
        
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private int _cashReward;
        [SerializeField] private Poolable _moneyImagePrefab;
        
        public string Name => _name;
        public string Description => _description;
        public int CashReward => _cashReward;
        public Poolable MoneyImagePrefab => _moneyImagePrefab;

        
        public static void InitializeValues() {
            var enemySos = Resources.LoadAll<EnemySO>("Enemy");
            foreach (var enemySo in enemySos) {
                enemySo.UpdateValuesRemote();
            }
        }

        public void UpdateValuesRemote() {
            _cashReward = RemoteManager.GetInt("ID_" + name + "CashReward");
        }
    }
}