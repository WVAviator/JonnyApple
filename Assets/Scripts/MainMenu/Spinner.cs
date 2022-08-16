using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour {

    public float spinSpeed = 1;

    void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(0, 0, Mathf.Repeat(transform.eulerAngles.z + spinSpeed, 360));
    }
}
