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
                Debug.Log("Localization Loaded");
                localizationInitialized = true;
                HandleInitialize();
            };
            RemoteManager.OnInitialized += () => {
                Debug.Log("Initialized Remote Manager");
                Debug.Log(RemoteManager.GetString("nextUpdateDate"));
                remoteManager = true;
                HandleInitialize();
            };
            RemoteManager.Initialize();
            foreach (var obj in dontDestroy)
            {
                DontDestroyOnLoad(obj);
            }
        }

        private void HandleInitialize() {
            if (!localizationInitialized || !remoteManager) return;
            MobileNotificationManager.Initialize();
            
            SceneManager.LoadScene(baseSceneName, LoadSceneMode.Additive);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            Debug.Log("Finished Initialization");
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            RemoteManager.OnInitialized -= HandleInitialize;
        }
    }
}