using UnityEngine;
using System.Collections;

public class Joystick : MonoBehaviour {

    RectTransform rt;
    PrecisionAiming pa;
    int activeTouchId;
    bool activeTouch;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        pa = GameObject.FindGameObjectWithTag("Player").GetComponent<PrecisionAiming>();
    }

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(rt, touch.position)) continue;
            if (touch.phase == TouchPhase.Began) pa.BeginJoystickDrag(touch);          
            activeTouchId = touch.fingerId;
            activeTouch = true;
        }

        if (!activeTouch) return;

        foreach (Touch touch in Input.touches)
        {
            if (touch.fingerId != activeTouchId) continue;

            if (touch.phase == TouchPhase.Moved) pa.UpdateJoystickPosition();
            if (touch.phase == TouchPhase.Ended)
            {
                pa.EndJoystickDrag();
                activeTouch = false;
            }
        }
    }

    
}
