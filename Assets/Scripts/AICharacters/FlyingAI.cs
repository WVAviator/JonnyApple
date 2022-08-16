using UnityEngine;
using System.Collections;

public class FlyingAI : MonoBehaviour {

    public float maxDistance = 30;
    Vector2 homePosition;
    Vector2 destinationPosition;
    bool hasDestination;

    public LayerMask groundLayer;

    public Transform attackPoint;
    public float attackRange = 1.5f;
    public float damage = 15;
    public float knockback = 5;

    public int roamFactor = 100;
    public float groundAvoidDistance = 4;

    public int aggressionFactor = 100;
    public float playerDetectionRange = 25;
    Transform player;
    bool isAttacking;
    public float yAttackOffset = 3;

    public float roamSpeed = 15;
    public float attackSpeed = 20;

    public int activationDistance = 100;

    bool facingRight;

    Animator anim;
    Rigidbody2D rb;

    void Start()
    {
        homePosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Vector2.SqrMagnitude(Camera.main.transform.position - transform.position) > activationDistance * activationDistance)
        {
            rb.velocity = Vector2.zero;
            return;
        }


        anim.SetBool("isAttacking", (isAttacking && hasDestination));

        Roam();
        DetectPlayer();
        AvoidGround();

        MoveToDestination();

    }

    void Roam()
    {
        if (Random.Range(0, roamFactor) == 1 && !hasDestination)
        {
            destinationPosition = new Vector2(Random.Range((homePosition.x - maxDistance), (homePosition.x + maxDistance)),
                                            Random.Range((homePosition.y - maxDistance), (homePosition.y + maxDistance)));
            hasDestination = true;
         }

        if (Vector2.Distance(transform.position, homePosition) > maxDistance)
        {
            destinationPosition = homePosition;
            hasDestination = true;
        }
    }

    void AvoidGround()
    {
        if (Physics2D.OverlapCircle(transform.position, groundAvoidDistance, groundLayer))
        {
            hasDestination = false;
            Roam();
        }
    }

    void Flip()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }

    void MoveToDestination()
    {
        if (!hasDestination) return;
        if (destinationPosition.x < transform.position.x && facingRight) Flip();
        if (destinationPosition.x > transform.position.x && !facingRight) Flip();
        rb.velocity = (destinationPosition - (Vector2)transform.position).normalized * (isAttacking ? attackSpeed : roamSpeed);
        hasDestination = !(Vector2.Distance(transform.position, destinationPosition) < 0.5);
        if (!hasDestination && isAttacking) Attack();       
    }

    void Attack()
    {
        if (Vector2.Distance(attackPoint.position, player.position) <= attackRange) player.GetComponent<Health>().addDamage(damage);
        player.GetComponent<Rigidbody2D>().velocity += new Vector2(Random.Range(-knockback, knockback), Random.Range(-knockback, knockback));
    }

    void DetectPlayer()
    {
        isAttacking = (Vector2.Distance(transform.position, player.position) <= playerDetectionRange);

        if (isAttacking && Random.Range(0, aggressionFactor) == 1 && !hasDestination)
        {
            destinationPosition = new Vector2(player.position.x, player.position.y + yAttackOffset);
            hasDestination = true;
        }

    }


}
