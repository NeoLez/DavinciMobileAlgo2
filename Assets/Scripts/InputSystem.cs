using System;
using UnityEngine;

namespace Root {
    public class InputSystem : MonoBehaviour {
    public static InputSystem Instance {
        get;
        private set;
    }
    private void Awake() {
        Instance = this;
        DontDestroyOnLoad(this);
        _mouseTouch = new Touch();
        _mouseTouch.phase = TouchPhase.Ended;
        _touches[2] = new CustomTouch(Vector2.zero, CustomTouchPhase.Ended);
    }

    private readonly CustomTouch[] _touches = new CustomTouch[3];
    private Touch _mouseTouch;

    [SerializeField] private float dragDistance;
    [SerializeField] private float holdTime;

    public event Action<CustomTouch> OnTap;
    public event Action<CustomTouch> OnHold;
    public event Action<CustomTouch> OnDrag;
    
    
    public void Update() {
        HandleMouse(ref _mouseTouch);
        if (Input.touchCount > 0) {
            for (int i = 0; i < Input.touchCount && i < 2; i++) {
                HandleTouch(Input.touches[i], i);
            }
        }
        HandleTouch(_mouseTouch, 2);
    }

    private void HandleMouse(ref Touch touch) {
        if (Input.GetMouseButton(0)) {
            if (touch.phase == TouchPhase.Ended) {
                touch.phase = TouchPhase.Began;
                touch.position = Input.mousePosition;
                touch.deltaPosition = Vector2.zero;
            }
            else {
                if ((Vector2)Input.mousePosition == touch.position) {
                    touch.phase = TouchPhase.Stationary;
                    touch.deltaPosition = Vector2.zero;
                }
                else {
                    touch.phase = TouchPhase.Moved;
                    touch.deltaPosition = touch.position - (Vector2)Input.mousePosition;
                }

                touch.position = Input.mousePosition;
            }
        }
        else {
            touch.phase = TouchPhase.Ended;
        }
    }
    
    private void HandleTouch(Touch touch, int i) {
        switch (touch.phase) {
            case TouchPhase.Began:
                _touches[i] = new CustomTouch(touch.position);
                break;
            case TouchPhase.Moved:
                _touches[i].CurrentPosition = touch.position;
                if (_touches[i].Phase == CustomTouchPhase.Hold) {
                    _touches[i].OnPositionChanged?.Invoke(_touches[i].CurrentPosition);
                }
                if (_touches[i].Phase != CustomTouchPhase.Hold && 
                    (_touches[i].CurrentPosition - _touches[i].InitialPosition).magnitude > dragDistance) {
                    _touches[i].Phase = CustomTouchPhase.Drag;
                }
                break;
            case TouchPhase.Stationary:
                _touches[i].CurrentPosition = touch.position;
                if (_touches[i].Phase != CustomTouchPhase.Hold && Time.time - _touches[i].TimeStarted > holdTime) {
                    _touches[i].Phase = CustomTouchPhase.Hold;
                    OnHold?.Invoke(_touches[i]);
                }
                break;
            case TouchPhase.Ended:
                _touches[i].CurrentPosition = touch.position;
                switch (_touches[i].Phase) {
                    case CustomTouchPhase.Tap:
                        OnTap?.Invoke(_touches[i]);
                        break;
                    case CustomTouchPhase.Drag:
                        OnDrag?.Invoke(_touches[i]);
                        break;
                    case CustomTouchPhase.Hold:
                        _touches[i].OnEnded?.Invoke();
                        break;
                }
                _touches[i].Phase = CustomTouchPhase.Ended;
                break;
        }
    }

    public class CustomTouch {
        public float TimeStarted;
        public Vector2 InitialPosition;
        public Vector2 CurrentPosition;
        public CustomTouchPhase Phase;
        public Action<Vector2> OnPositionChanged;
        public Action OnEnded;

        public CustomTouch(Vector2 initialPosition, CustomTouchPhase phase = CustomTouchPhase.Tap) {
            TimeStarted = Time.time;
            InitialPosition = initialPosition;
            CurrentPosition = initialPosition;
            Phase = phase;
        }
    }

    public enum CustomTouchPhase {
        Tap,
        Drag,
        Hold,
        Ended,
    }
}
}