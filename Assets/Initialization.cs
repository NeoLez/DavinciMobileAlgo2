using System.Collections;
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
        [SerializeField] private GameObject loadingScreen;
        private bool localizationInitialized;
        private bool remoteManager;

        private void Awake()
        {
          
            localization.OnUpdate += HandleLocalizationLoaded;

            RemoteManager.OnInitialized += () => {
                Debug.Log("Initialized Remote Manager");
                remoteManager = true;
                HandleInitialize();
            };
            RemoteManager.Initialize();
            foreach (var obj in dontDestroy)
            {
                DontDestroyOnLoad(obj);
            }
        }

        
        private void HandleLocalizationLoaded()
        {
            Debug.Log("Localization Loaded");
            localizationInitialized = true;
            StartCoroutine(HandleInitialize());
        }

        private IEnumerator HandleInitialize()
        {
            
            if (!localizationInitialized || !remoteManager) yield break;
            MobileNotificationManager.Initialize();
            EnemySO.InitializeValues();
            TowerSO.InitializeValues();
            
            var a = SceneManager.LoadSceneAsync(baseSceneName, LoadSceneMode.Additive);
            var b = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            b.allowSceneActivation = false;
            
            while (!a.isDone) {
                yield return null;
            }
            
            b.allowSceneActivation = true;

            while (!b.isDone) {
                yield return null;
            }
            
            loadingScreen.SetActive(false);
            
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
            SceneManager.UnloadSceneAsync(gameObject.scene);
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(baseSceneName));
            Debug.Log("Finished Initialization");
        }

        private void HandleInitializeCall() {
            StartCoroutine(HandleInitialize());
        }

        private void OnDestroy()
        {
            RemoteManager.OnInitialized -= HandleInitializeCall;

            
            if (localization != null)
            {
                localization.OnUpdate -= HandleLocalizationLoaded;
            }
        }
    }
}