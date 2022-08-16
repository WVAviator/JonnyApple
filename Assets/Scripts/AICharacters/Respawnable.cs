using UnityEngine;
using System.Collections;

public class Respawnable : MonoBehaviour {

    RespawnHandler rh;
    Health objectHealth;

    public int respawnTime = 30;

    int health;
    Vector2 startPosition;

    void Start()
    {
        rh = GameObject.FindGameObjectWithTag("RespawnHandler").GetComponent<RespawnHandler>();
        startPosition = transform.position;
        objectHealth = GetComponent<Health>();
        health = (int)objectHealth.maxHealth;
    }

    void Update()
    {
        if (health == objectHealth.health) return;
        health = (int)objectHealth.health;
        if (health <= 0) MarkForRespawn();
    }

    void MarkForRespawn()
    {
        rh.InitiateRespawn(gameObject, startPosition, respawnTime);
    }
}
