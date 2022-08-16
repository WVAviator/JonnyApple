using UnityEngine;
using System.Collections;

public class PhysicalAttackAI : BaseAI {

    public float attackDamage = 25;
    public float checkRange = 0.5f;
    public Transform attackCheck;
    public float attackForce = 5;

    Health playerHealth;
    Rigidbody2D playerRB;

    void Start()
    {
        playerHealth = player.GetComponent<Health>();
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    protected override void Attack()
    {
        if (!(Physics2D.OverlapCircle(attackCheck.position, checkRange, playerLayer))) return;

        anim.SetTrigger("attackTrigger");
        playerHealth.addDamage(attackDamage);
        playerRB.velocity = (player.position - attackCheck.position).normalized * attackForce;

    }

}
