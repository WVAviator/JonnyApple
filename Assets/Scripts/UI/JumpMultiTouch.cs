using UnityEngine;
using System.Collections;

public class JumpMultiTouch : MonoBehaviour {

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Began) continue;

            if (RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), touch.position))
            GameObject.FindGameObjectWithTag("Player").GetComponent<MyPlayerController>().Jump();
        }
    }
}


