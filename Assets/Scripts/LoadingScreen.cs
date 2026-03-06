using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Root {
    public class LoadingScreen : MonoBehaviour {
        public static LoadingScreen Instance { get; private set; }
        [SerializeField] private GameObject canvas;

        private void Awake() {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            canvas.SetActive(false);
        }

        public void LoadScene(string scene) {
            gameObject.SetActive(true);
            canvas.SetActive(true);
            var op = SceneManager.LoadSceneAsync(scene);
            op.completed += operation => {
                canvas.SetActive(false);
            };
        }
    }
}