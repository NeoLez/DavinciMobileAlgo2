using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using UnityEngine;

namespace Root.Utils {
    public static class RemoteManager {
        private static bool _isInitialized;
        public static event Action OnInitialized;
        private struct userAttributes { }
        private struct appAttributes { }
        private static async Task InitializeRemoteConfigAsync() {
            await UnityServices.InitializeAsync();

            if (!AuthenticationService.Instance.IsSignedIn) {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
        }
        
        public static async Task Initialize() {
            Debug.Log("Initializing Remote Manager");
            if (Utilities.CheckForInternetConnection()) {
                await InitializeRemoteConfigAsync();
            }
            
            RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
            _isInitialized = true;
            OnInitialized?.Invoke();
        }

        public static bool IsInitialized => _isInitialized;


        public static int GetInt(string key) {
            return RemoteConfigService.Instance.appConfig.GetInt(key);
        }
    
        public static float GetFloat(string key) {
            return RemoteConfigService.Instance.appConfig.GetFloat(key);
        }
    
        public static bool GetBool(string key) {
            return RemoteConfigService.Instance.appConfig.GetBool(key);
        }
        
        public static string GetString(string key) {
            return RemoteConfigService.Instance.appConfig.GetString(key);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Reset() {
            _isInitialized = false;
            OnInitialized = null;
        }
    }
}