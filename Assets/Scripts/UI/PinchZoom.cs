using UnityEngine;
using System.Collections;

public class PinchZoom : MonoBehaviour {

    public float zoomOutLimit = 13;
    public float zoomInLimit = 5;
    public float zoomSpeed = 1;

    RectTransform rt;

    float touchDistanceDelta;
    float touchDistance;

    bool freshEnter;
    bool activeZoom;

    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    void Update()
    {

        if (!TwoTouchesInZoomArea())
        {
            activeZoom = false;
            return;
        }
        CheckFreshEnter();
        activeZoom = true;
        ManageZoom(GetTwoTouchesInRect());

    }

    Touch[] GetTwoTouchesInRect()
    {
        Touch first = Input.GetTouch(0);
        Touch second = Input.GetTouch(0);
        bool firstT = false;
        foreach (Touch touch in Input.touches)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(rt, touch.position))
            {
                if (!firstT)
                {
                    first = touch;
                    firstT = true;
                }
                if (firstT) second = touch;
            }
        }
        return new Touch[2] { first, second };
    }

    void CheckFreshEnter()
    {
        freshEnter = (TwoTouchesInZoomArea() && !activeZoom);
    }

    bool TwoTouchesInZoomArea()
    {
        int touchCount = 0;

        foreach (Touch touch in Input.touches)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(rt, touch.position)) touchCount++;
        }
        return (touchCount == 2);
    }

    void ManageZoom(Touch[] touches)
    {
        float distance = Vector2.Distance(touches[0].position, touches[1].position);
        touchDistanceDelta = ((touches[0].phase == TouchPhase.Began) || (touches[1].phase == TouchPhase.Began) || freshEnter) ? 0 : (distance - touchDistance);
        touchDistance = distance;

        float currentSize = Camera.main.orthographicSize;
        float newSize = Mathf.Clamp(currentSize + (-touchDistanceDelta * zoomSpeed), zoomInLimit, zoomOutLimit);

        Camera.main.orthographicSize = newSize;
      
    }
}
