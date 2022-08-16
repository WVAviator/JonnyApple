using UnityEngine;
using System.Collections;

public class FloatingHealthBar : MonoBehaviour {

    float health;
    float previousHealth;
    float ratio;
    float maxHealth;
    bool lastFlip = false;
    bool thisFlip = false;

	void Start () {

        maxHealth = transform.parent.parent.GetComponent<Health>().maxHealth;
        ratio = 1 / maxHealth;

	}
	
	void Update () {

        health = transform.parent.parent.GetComponent<Health>().health;
        transform.localScale = new Vector2(health * ratio, transform.localScale.y);

        thisFlip = (transform.parent.parent.localScale.x < 0);
        //Debug.Log("Flip is: " + thisFlip);
        if (lastFlip != thisFlip) Flip();
        lastFlip = thisFlip;

	}

    void Flip()
    {
        Vector3 xScale = transform.parent.localScale;
        xScale.x *= -1;
        transform.parent.localScale = xScale;
    }
}
