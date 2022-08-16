using UnityEngine;
using System.Collections;

public class JelloSpring : MonoBehaviour {

    public float maxVelocity = 75;
    public float minVelocity = 40;
    public float bounceMultiplier = 1.1f;
    public AudioSource sound;

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.GetComponent<Rigidbody2D>() == null) return;
        Rigidbody2D rb = c.gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(-rb.velocity.y * bounceMultiplier, minVelocity, maxVelocity));
        SoundUtility.PlaySound(sound, 0.1f, true);
    }

}
