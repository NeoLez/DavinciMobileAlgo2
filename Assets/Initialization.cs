using System;
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
        private void Awake()
        {
            RemoteManager.OnInitialized += () => HandleInitialize();
            foreach (var obj in dontDestroy)
            {
                DontDestroyOnLoad(obj);
            }
        }

        private void HandleInitialize()
        {
            //Instantiate(Systems);
            SceneManager.LoadScene(baseSceneName, LoadSceneMode.Additive);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            Debug.Log("a");
            RemoteManager.OnInitialized -= HandleInitialize;
        }
    }
}