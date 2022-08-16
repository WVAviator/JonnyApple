using UnityEngine;
using System.Collections;

public class AppleExplode : MonoBehaviour {

    public float minX = -5;
    public float minY = 0;
    public float maxX = 5;
    public float maxY = 5;
    public float minSpin = -5;
    public float maxSpin = 5;

    void Start () {

        Rigidbody2D[] rigidbodies = gameObject.GetComponentsInChildren<Rigidbody2D>();
        foreach (Rigidbody2D rb in rigidbodies)
        {
            rb.velocity = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            rb.angularVelocity = Random.Range(minSpin, maxSpin);
        }

	}

}
