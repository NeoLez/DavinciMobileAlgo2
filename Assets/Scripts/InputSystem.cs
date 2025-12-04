using System;
using UnityEngine;

namespace Root {
    public class InputSystem : MonoBehaviour
    {
        public static InputSystem Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        public event Action<Vector2> OnTap;


        public void Update()
        {
            if (Input.touchCount == 0 && Input.GetMouseButtonDown(0))
            {
                Debug.Log("mouse");
                OnTap?.Invoke(Input.mousePosition);
            }

            if (Input.touchCount > 0)
            {
                foreach (var touch in Input.touches)
                {
                    Debug.Log($"{touch.fingerId} {touch.phase}");
                    if (touch.phase == TouchPhase.Began)
                    {
                        Debug.Log("tap");
                        OnTap?.Invoke(touch.position);
                    }
                }
            }
        }
    }
}