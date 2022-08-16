using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public float health;
    public float maxHealth;

    public bool respawnable = false;
    public int respawnTime = 20;
    public GameObject respawnPrefab;

    Animator anim;
    RespawnHandler rh;
    Vector2 startPos;

    void Awake()
    {
        maxHealth = health;
        anim = GetComponent<Animator>();
        rh = GameObject.FindGameObjectWithTag("RespawnHandler").GetComponent<RespawnHandler>();
        startPos = transform.position;
    }

    public void SetDead()
    {
        health = 0;
    }

    public void addDamage(float damage) {

        health -= damage;
        if (health <= 0 && respawnable)
        {
            rh.InitiateRespawn(respawnPrefab, startPos, respawnTime);
        }

    }

    public void addHealth(float heal)
    {
        health += heal;
        if (health > maxHealth) health = maxHealth;

    }

    void Death()
    {
        anim.SetTrigger("Death");
    }

    public void Restore()
    {
        health = maxHealth;
    }

}
