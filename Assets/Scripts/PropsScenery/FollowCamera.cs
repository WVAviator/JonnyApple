using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
	
    public float horizontalParallax = 1;
    public float verticalParallax = 0;
    Transform cam;
    Vector3 lastPosition;
    Vector3 camDelta;
    Vector3 startPosition;
    private bool firstTime = true;

    void Awake()
    {
        startPosition = transform.position;
        cam = Camera.main.transform;
    }

    void Update () {

        camDelta = cam.position - lastPosition;
        lastPosition = cam.position;       

        transform.position = new Vector3(transform.position.x + (camDelta.x * horizontalParallax), 
                                            transform.position.y + (camDelta.y * verticalParallax),
                                            transform.position.z);
        if (firstTime)
        {
            transform.position = startPosition;
            firstTime = false;
        }
	}
}
