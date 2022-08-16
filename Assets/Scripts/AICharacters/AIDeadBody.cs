using UnityEngine;
using System.Collections;

public class AIDeadBody : MonoBehaviour {

    public float minX = -10;
    public float maxX = 10;
    public float minY = 0;
    public float maxY = 10;
    public float rotation = 30;

	void Start()
    {
        foreach (Rigidbody2D rb in GetComponentsInChildren<Rigidbody2D>())
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            rb.velocity = new Vector2(randomX, randomY);
            rb.angularVelocity = Random.Range(-rotation, rotation);

        }
    }

}
