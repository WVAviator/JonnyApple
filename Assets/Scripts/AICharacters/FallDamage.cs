using UnityEngine;
using System.Collections;

public class FallDamage : MonoBehaviour {

    public float damageThreshold = 50;
    public float deathThreshold = 100;
    Health health;
    public LayerMask softGround;

    void Start()
    {
        health = GetComponent<Health>();
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.layer == softGround) return;
        float impact = c.relativeVelocity.magnitude;
		if (impact < damageThreshold)
			return;
		float damage = (health.maxHealth / (deathThreshold - damageThreshold)) * (impact - damageThreshold);
		health.addDamage(damage);
    }
}
