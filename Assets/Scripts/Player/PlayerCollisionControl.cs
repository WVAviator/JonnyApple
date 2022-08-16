using UnityEngine;
using System.Collections;

public class PlayerCollisionControl : MonoBehaviour {

    public float damageThreshold = 30;
    public float deathThreshold = 50;
    public float damageMultiplier = 3;

    void OnCollisionEnter2D(Collision2D c)
    {

        
        if (Mathf.Abs(c.relativeVelocity.y) >= deathThreshold)
        {
            if (gameObject.GetComponent<Health>() != null)
            {
                gameObject.GetComponent<Health>().SetDead();
            }
            return;
        }

       if (Mathf.Abs(c.relativeVelocity.y) >= damageThreshold)
        {
            float damage = (1 / (deathThreshold - damageThreshold) * damageMultiplier * Mathf.Abs(c.relativeVelocity.y));
            if (gameObject.GetComponent<Health>() != null)
            {
                gameObject.GetComponent<Health>().addDamage(damage);
            }
            return;
        }

    }
}
