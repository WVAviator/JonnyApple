using UnityEngine;
using System.Collections;

public class Invincibility : MonoBehaviour {

    public GameObject particles;
    Health health;
    public bool isInvincible;
    public int invincibleTime = 20;
    float timeCount;

    void Start()
    {
        health = GetComponent<Health>();
    }

    public void ActivateInvincibility()
    {
        isInvincible = true;
        timeCount = invincibleTime;
    }

    void Update()
    {
        if (isInvincible) health.health = health.maxHealth;
        particles.SetActive(isInvincible);
    }

    void FixedUpdate()
    {
        if (isInvincible) timeCount -= 0.02f;
        if (timeCount <= 0) isInvincible = false;
    }
}
