using UnityEngine;
using System.Collections;

public class MenuClouds : MonoBehaviour {

    public float cloudSpeed = 0.01f;

    void FixedUpdate()
    {
        
        transform.position = new Vector2(transform.position.x + cloudSpeed, transform.position.y);

        if (transform.position.x >= 34) transform.position = new Vector2(-31, transform.position.y);

    }
}
