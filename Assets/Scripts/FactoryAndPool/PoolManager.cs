using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Root.FactoryAndPool {
    public class PoolManager : MonoBehaviour {
        public static PoolManager Instance { get; private set; }
        private readonly Dictionary<Poolable, Queue<Poolable>> _pools = new();

        private void Awake() {
            Instance = this;
        }
        
        public Poolable GetObject(Poolable prefab) {
            if (!_pools.TryGetValue(prefab, out var pool)) {
                pool = new();
                _pools[prefab] = pool;
            }

            if (!pool.TryDequeue(out Poolable obj)) {
                obj = Instantiate(prefab);
                obj.SetPrefab(prefab);
                obj.Initialize();
            }
            obj.TurnOn();
            return obj;
        }

        public void ReturnObjectToPool(Poolable obj) {
            Debug.Log(obj);
            Debug.Log(obj.GetPrefab());
            if (!_pools.TryGetValue(obj.GetPrefab(), out var pool)) {
                Assert.IsTrue(false, "Trying to return a poolable object to a non existent pool");
                return;
            }
            pool.Enqueue(obj);
        }
    }
}