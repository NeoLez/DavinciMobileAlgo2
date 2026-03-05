using System.Collections.Generic;
using Root.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Root
{
    public class Initialization : MonoBehaviour
    {
        [SerializeField] string sceneName;
        [SerializeField] string baseSceneName;
        [SerializeField] private List<GameObject> dontDestroy;
        [SerializeField] private Localization localization;
        private bool localizationInitialized;
        private bool remoteManager;
        private void Awake()
        {
            localization.OnUpdate += () => {
                localizationInitialized = true;
                HandleInitialize();
            };
            RemoteManager.OnInitialized += () => {
                remoteManager = true;
                HandleInitialize();
            };
            foreach (var obj in dontDestroy)
            {
                DontDestroyOnLoad(obj);
            }
        }

        private void HandleInitialize() {
            if (!localizationInitialized || !remoteManager) return;
            
            SceneManager.LoadScene(baseSceneName, LoadSceneMode.Additive);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            RemoteManager.OnInitialized -= HandleInitialize;
        }
    }
}