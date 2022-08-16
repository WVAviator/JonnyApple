using UnityEngine;
using System.Collections;

public class PrecisionAiming : MonoBehaviour {

    bool isDragging;
    Animator anim;
    Transform joystick;
    Vector2 joystickPosition;
    public float joystickStretch = 50;
    MyPlayerController mpc;
    public Transform rightArm;
    public Transform leftArm;
    float screenRatio;
    int activeTouchId;

    void Start()
    {
        anim = GetComponent<Animator>();
        joystick = GameObject.FindGameObjectWithTag("Joystick").transform;
        joystickPosition = joystick.position;
        mpc = GetComponent<MyPlayerController>();
        screenRatio = (Screen.height / GameObject.FindGameObjectWithTag("UICanvas").GetComponent<RectTransform>().rect.height);
    }

    void Update()
    {     
        anim.SetBool("CenterJoystick", isDragging);
    }

    void LateUpdate()
    {
        if (isDragging) Aim();
    }

    public void BeginJoystickDrag(Touch touch)
    {
        isDragging = true;
        activeTouchId = touch.fingerId;
    }

    public void EndJoystickDrag()
    {
        isDragging = false;
        joystick.position = joystickPosition;
    }

    public void UpdateJoystickPosition()
    {      
        Vector2 mp = Input.GetTouch(activeTouchId).position;
        Vector2 updatedMousePosition = mp - joystickPosition;
        updatedMousePosition = Vector2.ClampMagnitude(updatedMousePosition, joystickStretch * screenRatio);
        joystick.position = updatedMousePosition + joystickPosition;
            
    }

    public void Aim()
    {
        float angle = Mathf.Atan2(joystick.position.y - joystickPosition.y, joystick.position.x - joystickPosition.x) * 180 / Mathf.PI;

        if (((angle > 90) || (angle < -90)) && mpc.facingRight) mpc.Flip();
        if (((angle < 90) && (angle > -90)) && !mpc.facingRight) mpc.Flip();

        if (mpc.facingRight)
            rightArm.eulerAngles = new Vector3(0, 0, Mathf.Repeat(angle + 360, 360));
        if (!mpc.facingRight)
            rightArm.eulerAngles = new Vector3(0, 0, -Mathf.Repeat(angle + 180, 360));
        leftArm.eulerAngles = rightArm.eulerAngles;
    }


}
