using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Root {
    public class Click : MonoBehaviour {
        private void Start() {
            InputSystem.Instance.OnTap += Process;
        }

        private void Process(InputSystem.CustomTouch touch) {
            if(!Physics.Raycast(Camera.main.ScreenPointToRay(touch.CurrentPosition), out var hit)) return;
            Vector2 posGrid = new Vector2(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y));
            Debug.Log(posGrid);
        }
    }
}